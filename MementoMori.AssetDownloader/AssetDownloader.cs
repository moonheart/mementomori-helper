using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using MementoMori;
using MementoMori.AssetDownloader.Alist;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace ConsoleApp1;

internal class AssetDownloader : BackgroundService
{
    private const string processedAssetsPath = "./Assets/";
    private const string assetsToBeExtractPath = "./Assets-tmp/";
    private const string exportedAssetsPath = "./Assets-export/";
    private const string apkDownloadPath = "./apks/";
    private const string apkExtractPath = "./apks-extract/";
    private readonly DownloaderOption _downloaderOption;

    private readonly HttpClient _httpClient = new(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        })
        {DefaultRequestHeaders = {{"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36"}}};

    private readonly ILogger<AssetDownloader> _logger;

    private readonly MementoNetworkManager _networkManager;

    public AssetDownloader(MementoNetworkManager networkManager, IOptions<DownloaderOption> downloaderOption, ILogger<AssetDownloader> logger)
    {
        _networkManager = networkManager;
        _networkManager.DisableAutoUpdateMasterData = true;
        _downloaderOption = downloaderOption.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var oldCwd = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(_downloaderOption.WorkingDir);
            try
            {
                await DownloadRawDataFromCdn(stoppingToken);
                await DownloadAssetsFromBoi(stoppingToken);
                await DownloadAssetsInApk(stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Directory.SetCurrentDirectory(oldCwd);
            }

            await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
        }
    }

    private async Task DownloadRawDataFromCdn(CancellationToken stoppingToken)
    {
        // https://cdn-mememori.akamaized.net/asset/MementoMori/Raw/MusicPlayer/CHR_000048_SONG_JP.mp3
        var catalogUrl = "https://github.com/moonheart/mementomori-masterbook/raw/refs/heads/master/Master/DownloadRawDataMB.json";
        var rawDatas = await _httpClient.GetFromJsonAsync<List<RawData>>(catalogUrl);
        if (rawDatas == null)
        {
            _logger.LogError("Failed to get raw data from cdn");
            return;
        }

        var aListApi = new AListApi(_downloaderOption.AListUrl);
        await aListApi.AuthLogin(_downloaderOption.AlistUsername, _downloaderOption.AlistPassword);
        var subDir = "Other/RawData";
        foreach (var rawData in rawDatas)
        {
            if (stoppingToken.IsCancellationRequested)
                return;
            var targetFile = Path.Combine(_downloaderOption.AListTargetPath, subDir, rawData.FilePath);
            var fsGetResponse = await aListApi.FsGet(targetFile);
            if (fsGetResponse != null && fsGetResponse.Size == rawData.FileSize)
            {
                _logger.LogInformation($"Skip {rawData.FilePath}");
                continue;
            }

            _logger.LogInformation($"Downloading {rawData.FilePath}");
            var fileBytes = await _httpClient.GetByteArrayAsync($"https://cdn-mememori.akamaized.net/asset/MementoMori/Raw/{rawData.FilePath}");
            _logger.LogInformation($"Uploading to {targetFile}");
            await aListApi.FsPut(targetFile, fileBytes, "application/octet-stream");
        }
    }


    private async Task ConvertAndUpload(CancellationToken stoppingToken)
    {
        if (Directory.Exists(exportedAssetsPath))
            Directory.Delete(exportedAssetsPath, true);

        var processStartInfo = new ProcessStartInfo
        {
            FileName = _downloaderOption.AssetStutioCliPath,
            Arguments = $"{assetsToBeExtractPath} -t {_downloaderOption.ExportAssetType} -o {exportedAssetsPath}",
            WorkingDirectory = Directory.GetCurrentDirectory()
        };

        _logger.LogInformation($"Start to execute {processStartInfo.FileName} {processStartInfo.Arguments}");
        _logger.LogInformation("This may take a long time, please wait patiently.");

        var process = Process.Start(processStartInfo);
        if (process != null)
        {
            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            await process.WaitForExitAsync(stoppingToken);
            if (process.ExitCode != 0)
            {
                _logger.LogError($"Failed to execute {processStartInfo.FileName} {processStartInfo.Arguments}");
                _logger.LogError(process.StandardOutput.ReadToEnd());
                _logger.LogError(process.StandardError.ReadToEnd());
                throw new Exception("Failed to execute AssetStudioCLI");
            }
        }

        // delete duplicate files, end with _#\d+
        foreach (var file in Directory.GetFiles(exportedAssetsPath, "*", SearchOption.AllDirectories))
        {
            var filename = Path.GetFileNameWithoutExtension(file);
            if (Regex.IsMatch(filename, @"_#\d+$"))
            {
                _logger.LogInformation($"Delete duplicate file {file}");
                File.Delete(file);
            }
        }


        var aListApi = new AListApi(_downloaderOption.AListUrl);
        await aListApi.AuthLogin(_downloaderOption.AlistUsername, _downloaderOption.AlistPassword);
        var fsGetResponse = await aListApi.FsGet(_downloaderOption.AListTargetPath);
        if (fsGetResponse == null)
            // not found, create directory
            await aListApi.FsMkdir(_downloaderOption.AListTargetPath);

        // upload files to AList
        _logger.LogInformation("Upload files to AList");
        await CopyFilesRecursively(aListApi, new DirectoryInfo(exportedAssetsPath), _downloaderOption.AListTargetPath, true, stoppingToken);

        // copy temp files in ./Assets-tmp to ./Assets
        _logger.LogInformation($"Copy temp files to {processedAssetsPath}");
        foreach (var file in Directory.GetFiles(assetsToBeExtractPath))
        {
            var targetFile = Path.Combine(processedAssetsPath, Path.GetFileName(file));
            if (File.Exists(targetFile))
                File.Delete(targetFile);

            File.Move(file, targetFile);
        }

        // delete temp files
        _logger.LogInformation("Delete temp files");
        Directory.Delete(assetsToBeExtractPath, true);
        Directory.Delete("./Assets-export", true);
    }

    private async Task DownloadAssetsFromBoi(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start to download assets");
        var isDownloaded = false;
        while (true)
        {
            try
            {
                if (!_downloaderOption.SkipDownloadFromBoi)
                {
                    await _networkManager.Initialize(s => _logger.LogInformation(s));
                    await _networkManager.DownloadAssets(_downloaderOption.GameOs, processedAssetsPath, assetsToBeExtractPath, stoppingToken);
                }

                isDownloaded = true;
                break;
            }
            catch (TaskCanceledException e)
            {
                _logger.LogInformation("Download assets timeout, retry");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to download assets, abort");
                break;
            }
        }

        if (isDownloaded)
        {
            var files = Directory.GetFiles(assetsToBeExtractPath);
            if (files.Length == 0)
            {
                _logger.LogInformation("No assets from boi to be downloaded, skip");
                return;
            }

            await SendNotification($"Assets 有更新, 共更新了 {files.Length} 个文件");
            await ConvertAndUpload(stoppingToken);
            _logger.LogInformation("Download assets from boi finished");
        }
    }

    private static async Task SendNotification(string message)
    {
        var token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
        var chatId = Environment.GetEnvironmentVariable("TELEGRAM_CHAT_ID");
        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(chatId))
            return;
        TelegramBotClient botClient = new(token);
        await botClient.SendMessage(chatId, message);
    }


    private async Task DownloadAssetsInApk(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start to download assets in apk");
        var lastVersion = "none";
        if (File.Exists(_downloaderOption.ApkVersionFile))
            lastVersion = (await File.ReadAllTextAsync(_downloaderOption.ApkVersionFile)).Trim();
        _logger.LogInformation($"Last version is {lastVersion}");

        var html = await _httpClient.GetStringAsync("https://mememori-game.com/apps/vars.js");
        // '/apps/mementomori_2.14.1.apk'
        var match = Regex.Match(html, @"/apps/mementomori_(?<version>\d+.\d+.\d+).apk");
        if (!match.Success)
        {
            _logger.LogError("Failed to get version from https://mememori-game.com/apps/vars.js");
            return;
        }

        var version = match.Groups["version"].Value.Trim();
        _logger.LogInformation($"Current version is {version}");
        if (version == lastVersion)
        {
            _logger.LogInformation("Apk version is not changed, skip");
            return;
        }

        var apkUrl = $"https://mememori-game.com/apps/mementomori_{version}.apk";
        if (!Directory.Exists(apkDownloadPath))
            Directory.CreateDirectory(apkDownloadPath);
        var apkPath = Path.Combine(apkDownloadPath, $"mementomori_{version}.apk");

        if (!File.Exists(apkPath))
        {
            _logger.LogInformation($"Downloading apk from {apkUrl}");
            await using (var apkStream = await _httpClient.GetStreamAsync(apkUrl))
            {
                await using (var fileStream = File.Create(apkPath))
                {
                    await apkStream.CopyToAsync(fileStream, stoppingToken);
                }
            }
        }

        _logger.LogInformation("Extracting assets from apk");

        // unzip file
        Directory.CreateDirectory(apkExtractPath);
        ZipFile.ExtractToDirectory(apkPath, apkExtractPath, true);

        // copy files in ./apk-tmp/assets/aa/Android to ./Assets-tmp, if file exists in ./Assets, skip
        _logger.LogInformation($"Copy files to {assetsToBeExtractPath}");
        if (!Directory.Exists(assetsToBeExtractPath))
            Directory.CreateDirectory(assetsToBeExtractPath);
        foreach (var file in Directory.GetFiles(Path.Combine(apkExtractPath, "assets/aa/Android/")))
        {
            var finalPath = Path.Combine(processedAssetsPath, Path.GetFileName(file));
            var finalFile = new FileInfo(finalPath);
            if (finalFile.Exists && finalFile.Length == new FileInfo(file).Length)
                continue;

            var tmpPath = Path.Combine(assetsToBeExtractPath, Path.GetFileName(file));
            File.Copy(file, tmpPath, true);
        }

        var files = Directory.GetFiles(assetsToBeExtractPath);
        if (files.Length == 0)
        {
            _logger.LogInformation("No assets in apk to be downloaded, skip");
            return;
        }

        await SendNotification($"客户端有更新, {lastVersion}->{version}, 共更新了 {files.Length} 个文件");

        await ConvertAndUpload(stoppingToken);

        // save version
        await File.WriteAllTextAsync(_downloaderOption.ApkVersionFile, version, stoppingToken);
        Directory.Delete(apkExtractPath, true);
        _logger.LogInformation("Download assets in apk finished");
    }

    private async Task CopyFilesRecursively(AListApi aListApi, DirectoryInfo source, string targetPath, bool checkExist, CancellationToken ct)
    {
        foreach (var dir in source.GetDirectories())
        {
            if (ct.IsCancellationRequested)
                return;
            var target = Path.Combine(targetPath, dir.Name);
            var fsGetResponse = await aListApi.FsGet(target);
            if (fsGetResponse == null)
            {
                _logger.LogInformation($"Creating directory {target}");
                // not found, create directory
                await aListApi.FsMkdir(target);
            }

            await CopyFilesRecursively(aListApi, dir, target, checkExist, ct);
        }

        var listReponse = await aListApi.FsList(targetPath);
        var existedFiles = checkExist ? listReponse?.Content?.Where(f => !f.IsDir).ToList() ?? [] : [];

        foreach (var file in source.GetFiles())
        {
            if (ct.IsCancellationRequested)
                return;
            if (!_downloaderOption.ForceUploadFiles.Any(forceFile =>
                {
                    var fullPath = Path.GetFullPath(Path.Combine(exportedAssetsPath, forceFile));
                    return fullPath == file.FullName;
                }))
            {
                var existedFile = existedFiles.FirstOrDefault(f => f.Name == file.Name);
                if (existedFile != null && file.Length == existedFile.Size)
                {
                    _logger.LogInformation($"Skip {file.FullName}");
                    continue;
                }
            }

            var targetFile = Path.Combine(targetPath, file.Name);

            var contentType = file.Extension switch
            {
                ".png" => "image/png",
                ".wav" => "audio/wav",
                ".mp4" => "video/mp4",
                _ => "application/octet-stream"
            };
            _logger.LogInformation($"Uploading to {targetFile} with content type {contentType}");
            await aListApi.FsPut(targetFile, await File.ReadAllBytesAsync(file.FullName), contentType);
        }
    }
}

public record RawData(
    int Id,
    bool IsIgnore,
    string Memo,
    string FilePath,
    int FileSize,
    int RawDataDownloadType,
    string Etag
);