# open powershell with admin and run Set-ExecutionPolicy RemoteSigned

$ErrorActionPreference = "Stop"

# 设置GitHub仓库信息
$repository = "moonheart/mementomori-helper"
$releaseApiUrl = "https://api.github.com/repos/$repository/releases/latest"
$downloadUrl = "https://github.com/$repository/releases/download"

# 获取语言环境
$language = (Get-Culture).Name

# ANSI颜色定义
$Red = "Red"
$Green = "Green"
$Yellow = "Yellow"

# 消息翻译函数
function Translate-Message($messageKey, $params = @{}) {
    $messages = @{
        "AlreadyLatest" = @{
            "zh-CN" = "程序已经是最新版本 ($($params.currentVersionStr)). 无需更新。"
            "zh-TW" = "程式已經是最新版本 ($($params.currentVersionStr)). 無需更新。"
            "ja-JP" = "プログラムはすでに最新バージョンです ($($params.currentVersionStr))。更新は不要です。"
            "default" = "The program is already the latest version ($($params.currentVersionStr)). No update is necessary."
        }
        "StartUpdate" = @{
            "zh-CN" = "开始更新..."
            "zh-TW" = "開始更新..."
            "ja-JP" = "更新を開始します..."
            "default" = "Starting update..."
        }
        "UpdateSuccess" = @{
            "zh-CN" = "程序已成功更新到版本 $($params.latestVersionStr)."
            "zh-TW" = "程式已成功更新到版本 $($params.latestVersionStr)."
            "ja-JP" = "プログラムはバージョン $($params.latestVersionStr) に正常に更新されました。"
            "default" = "The program has been successfully updated to version $($params.latestVersionStr)."
        }
        "NoLocalVersion" = @{
            "zh-CN" = "未检测到本地程序版本，可能需要更新。"
            "zh-TW" = "未檢測到本地程式版本，可能需要更新。"
            "ja-JP" = "ローカルプログラムバージョンが検出されませんでした。更新が必要かもしれません。"
            "default" = "No local program version detected, update may be required."
        }
        "LatestVersion" = @{
            "zh-CN" = "最新版本: $($params.latestVersionStr)"
            "zh-TW" = "最新版本: $($params.latestVersionStr)"
            "ja-JP" = "最新バージョン: $($params.latestVersionStr)"
            "default" = "Latest Version: $($params.latestVersionStr)"
        }
        "CurrentVersion" = @{
            "zh-CN" = "当前版本: v$($params.cleanedCurrentVersionStr)"
            "zh-TW" = "當前版本: v$($params.cleanedCurrentVersionStr)"
            "ja-JP" = "現在のバージョン: v$($params.cleanedCurrentVersionStr)"
            "default" = "Current Version: v$($params.cleanedCurrentVersionStr)"
        }
    }

    if ($messages[$messageKey].ContainsKey($language)) 
    {
        return $messages[$messageKey][$language]
    } 
    else 
    {
        return $messages[$messageKey]["default"]
    }
}

# 指定程序文件名和下载目录
$programFileName = "MementoMori.WebUI.exe"
$downloadDirectory = $PSScriptRoot

# 删除上次的latest.zip和解压的临时目录（如果存在）
$lastDownloadedZip = Join-Path -Path $downloadDirectory -ChildPath "latest.zip"
$lastTempDir = Join-Path -Path $downloadDirectory -ChildPath "temp"

if (Test-Path -Path $lastDownloadedZip) 
{
    Remove-Item -Path $lastDownloadedZip -Force
}

if (Test-Path -Path $lastTempDir) 
{
    Remove-Item -Path $lastTempDir -Force -Recurse
}

# 获取最新的发布信息
$latestRelease = Invoke-RestMethod -Uri $releaseApiUrl
$latestVersionStr = $latestRelease.tag_name
Write-Host (Translate-Message "LatestVersion" @{ latestVersionStr = $latestVersionStr }) -ForegroundColor $Yellow

# 获取本地程序的版本信息
if (Test-Path -Path (Join-Path -Path $downloadDirectory -ChildPath $programFileName)) 
{
    $currentVersionInfo = (Get-ItemProperty -Path (Join-Path -Path $downloadDirectory -ChildPath $programFileName)).VersionInfo
    $currentVersionStr = $currentVersionInfo.ProductVersion

    $cleanedCurrentVersionStr = $currentVersionStr.Split('+')[0]

    Write-Host (Translate-Message "CurrentVersion" @{ cleanedCurrentVersionStr = $cleanedCurrentVersionStr }) -ForegroundColor $Yellow

    if ([Version]$cleanedCurrentVersionStr -ge [Version]$latestVersionStr.TrimStart("v")) 
    {
        Write-Host (Translate-Message "AlreadyLatest" @{ currentVersionStr = "v$cleanedCurrentVersionStr" }) -ForegroundColor $Green
        exit 0
    }
}
else 
{
    Write-Host (Translate-Message "NoLocalVersion") -ForegroundColor $Red
}

Write-Host (Translate-Message "StartUpdate") -ForegroundColor $Green

# 下载最新版本的压缩包
$zipName = "publish-win-x64.zip"
$downloadLink = "$downloadUrl/$latestVersionStr/$zipName"

$downloadPath = Join-Path -Path $downloadDirectory -ChildPath "latest.zip"
Invoke-WebRequest -Uri $downloadLink -OutFile $downloadPath

# 解压缩文件到临时文件夹
$tempDir = Join-Path -Path $downloadDirectory -ChildPath "temp"
Expand-Archive -Path $downloadPath -DestinationPath $tempDir -Force

# 停止当前运行的程序
Stop-Process -Name "MementoMori.WebUI" -ErrorAction SilentlyContinue

# 复制解压后的文件到程序文件夹
Copy-Item -Path (Join-Path -Path $tempDir -ChildPath "publish-win-x64\*") -Destination $downloadDirectory -Recurse -Force

# 删去临时文件夹与下载的包
Remove-Item -Path $tempDir -Recurse -Force
Remove-Item -Path $downloadPath -Force

# 启动新程序
Start-Process -FilePath (Join-Path -Path $downloadDirectory -ChildPath $programFileName)

Write-Host (Translate-Message "UpdateSuccess" @{ latestVersionStr = $latestVersionStr }) -ForegroundColor $Green
