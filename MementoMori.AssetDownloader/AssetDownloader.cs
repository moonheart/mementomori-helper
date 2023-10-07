using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MementoMori;
using MementoMori.AssetDownloader.Alist;
using Microsoft.Extensions.Options;

namespace ConsoleApp1;

internal class AssetDownloader : BackgroundService
{
    private readonly MementoNetworkManager _networkManager;
    private readonly DownloaderOption _downloaderOption;
    private readonly ILogger<AssetDownloader> _logger;

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
                if (Directory.Exists("./Assets-export"))
                    Directory.Delete("./Assets-export", true);

                var processStartInfo = new ProcessStartInfo()
                {
                    FileName = _downloaderOption.AssetStutioCliPath,
                    Arguments = "./Assets-tmp -t tex2d,audio,video -o ./Assets-export",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
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

                var aListApi = new AListApi(_downloaderOption.AListUrl);
                await aListApi.AuthLogin(_downloaderOption.AlistUsername, _downloaderOption.AlistPassword);
                var fsGetResponse = await aListApi.FsGet(_downloaderOption.AListTargetPath);
                if (fsGetResponse == null)
                    // not found, create directory
                    await aListApi.FsMkdir(_downloaderOption.AListTargetPath);

                // upload files to AList
                _logger.LogInformation("Upload files to AList");
                await CopyFilesRecursively(aListApi, new DirectoryInfo("./Assets-export"), _downloaderOption.AListTargetPath);

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
                Directory.Delete("./Assets-export", true);

                _logger.LogInformation("Download assets finished");
            }

            await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
        }
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