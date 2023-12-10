using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MementoMori;
using MementoMori.AssetDownloader.Alist;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace ConsoleApp1;

internal class AssetDownloader : BackgroundService
{
    private readonly MementoNetworkManager _networkManager;
    private readonly DownloaderOption _downloaderOption;
    private readonly ILogger<AssetDownloader> _logger;

    private readonly HttpClient _httpClient = new(new HttpClientHandler()
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
    }) {DefaultRequestHeaders = {{"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36"}}};

    public AssetDownloader(MementoNetworkManager networkManager, IOptions<DownloaderOption> downloaderOption, ILogger<AssetDownloader> logger)
    {
        _networkManager = networkManager;
        _downloaderOption = downloaderOption.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DownloadAssetsFromBoi(stoppingToken);
            // await DownloadAssetsInApk(stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
        }
    }

    private async Task ConvertAndUpload(CancellationToken stoppingToken)
    {
        if (Directory.Exists("./Assets-export"))
            Directory.Delete("./Assets-export", true);

        var processStartInfo = new ProcessStartInfo()
        {
            FileName = _downloaderOption.AssetStutioCliPath,
            Arguments = "./Assets-tmp -t tex2d,audio,video -o ./Assets-export",
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

        // var aListApi = new AListApi(_downloaderOption.AListUrl);
        // await aListApi.AuthLogin(_downloaderOption.AlistUsername, _downloaderOption.AlistPassword);
        // var fsGetResponse = await aListApi.FsGet(_downloaderOption.AListTargetPath);
        // if (fsGetResponse == null)
        //     // not found, create directory
        //     await aListApi.FsMkdir(_downloaderOption.AListTargetPath);
        //
        // // upload files to AList
        // _logger.LogInformation("Upload files to AList");
        // await CopyFilesRecursively(aListApi, new DirectoryInfo("./Assets-export"), _downloaderOption.AListTargetPath);

        // copy temp files in ./Assets-tmp to ./Assets
        _logger.LogInformation($"Copy temp files to {_downloaderOption.DownloadPath}");
        foreach (var file in Directory.GetFiles("./Assets-tmp"))
        {
            var targetFile = Path.Combine(_downloaderOption.DownloadPath, Path.GetFileName(file));
            if (File.Exists(targetFile))
                File.Delete(targetFile);

            File.Move(file, targetFile);
        }

        // delete temp files
        _logger.LogInformation("Delete temp files");
        Directory.Delete("./Assets-tmp", true);
        // Directory.Delete("./Assets-export", true);
    }

    private async Task DownloadAssetsFromBoi(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start to download assets");
        var isDownloaded = false;
        while (true)
            try
            {
                await _networkManager.DownloadAssets(_downloaderOption.GameOs, _downloaderOption.DownloadPath, "./Assets-tmp", stoppingToken);
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

        if (isDownloaded)
        {
            var files = Directory.GetFiles("./Assets-tmp");
            if (files.Length == 0)
            {
                _logger.LogInformation("No assets downloaded, skip");
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
        await botClient.SendTextMessageAsync(chatId, message);
    }


    private async Task DownloadAssetsInApk(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Start to download assets in apk");
        var lastVersion = "none";
        if (File.Exists(_downloaderOption.ApkVersionFile))
            lastVersion = (await File.ReadAllTextAsync(_downloaderOption.ApkVersionFile)).Trim();
        _logger.LogInformation($"Last version is {lastVersion}");

        var html = await _httpClient.GetStringAsync("https://apkpure.com/mementomori-afkrpg/jp.boi.mementomori.android");
        var match = Regex.Match(html, @"version_name:\s+'(?<version>.+?)'");
        if (!match.Success)
        {
            _logger.LogError("Failed to get version from apkpure");
            return;
        }

        var version = match.Groups["version"].Value.Trim();
        _logger.LogInformation($"Current version is {version}");
        if (version == lastVersion)
        {
            _logger.LogInformation("Apk version is not changed, skip");
            return;
        }

        var apkUrl = $"https://d.apkpure.com/b/XAPK/jp.boi.mementomori.android?version=latest";
        if (!Directory.Exists("./apks"))
            Directory.CreateDirectory("./apks");
        var apkPath = Path.Combine("./apks/", "mementomori.zip");
        if (File.Exists(apkPath))
            File.Delete(apkPath);

        _logger.LogInformation($"Downloading apk from {apkUrl}");
        await using (var apkStream = await _httpClient.GetStreamAsync(apkUrl))
        {
            await using (var fileStream = File.Create(apkPath))
            {
                await apkStream.CopyToAsync(fileStream, stoppingToken);
            }
        }

        _logger.LogInformation("Extracting assets from apk");

        // unzip file
        ZipFile.ExtractToDirectory(apkPath, "./apk-tmp", true);
        // check if UnityDataAssetPack.apk exist
        var unityDataAssetPackApkPath = "./apk-tmp/UnityDataAssetPack.apk";
        if (!File.Exists(unityDataAssetPackApkPath))
        {
            _logger.LogError("UnityDataAssetPack.apk not found in apk");
            return;
        }

        // unzip UnityDataAssetPack.apk
        ZipFile.ExtractToDirectory(unityDataAssetPackApkPath, "./apk-tmp", true);

        // copy files in ./apk-tmp/assets/aa/Android to ./Assets-tmp, if file exists in ./Assets, skip
        _logger.LogInformation("Copy files to ./Assets-tmp");
        if (!Directory.Exists("./Assets-tmp"))
            Directory.CreateDirectory("./Assets-tmp");
        foreach (var file in Directory.GetFiles("./apk-tmp/assets/aa/Android/"))
        {
            var finalPath = Path.Combine(_downloaderOption.DownloadPath, Path.GetFileName(file));
            if (File.Exists(finalPath))
                continue;

            var tmpPath = Path.Combine("./Assets-tmp", Path.GetFileName(file));
            File.Copy(file, tmpPath, true);
        }

        var files = Directory.GetFiles("./Assets-tmp");
        if (files.Length == 0)
        {
            _logger.LogInformation("No assets downloaded, skip");
            return;
        }

        await SendNotification($"客户端有更新, {lastVersion}->{version}, 共更新了 {files.Length} 个文件");

        await ConvertAndUpload(stoppingToken);

        // save version
        await File.WriteAllTextAsync(_downloaderOption.ApkVersionFile, version, stoppingToken);

        _logger.LogInformation("Download assets in apk finished");
    }

    private async Task CopyFilesRecursively(AListApi aListApi, DirectoryInfo source, string targetPath)
    {
        foreach (var dir in source.GetDirectories())
        {
            var target = Path.Combine(targetPath, dir.Name);
            var fsGetResponse = await aListApi.FsGet(target);
            if (fsGetResponse == null)
            {
                _logger.LogInformation($"Creating directory {target}");
                // not found, create directory
                await aListApi.FsMkdir(target);
            }

            await CopyFilesRecursively(aListApi, dir, target);
        }

        foreach (var file in source.GetFiles())
        {
            var targetFile = Path.Combine(targetPath, file.Name);
            var fsGetResponse = await aListApi.FsGet(targetFile);
            if (fsGetResponse != null)
                // found, skip
            {
                _logger.LogInformation($"Skip {file.FullName} because it already exists in {targetFile}");
                continue;
            }

            var contentType = file.Extension switch
            {
                ".png" => "image/png",
                ".wav" => "audio/wav",
                ".mp4" => "video/mp4",
                _ => "application/octet-stream"
            };
            _logger.LogInformation($"Uploading {file.FullName} to {targetFile} with content type {contentType}");
            await aListApi.FsPut(targetFile, await File.ReadAllBytesAsync(file.FullName), contentType);
        }
    }
}