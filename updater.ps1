$ErrorActionPreference = "Stop"

# 设置GitHub仓库信息
$repository = "moonheart/mementomori-helper"
$releaseApiUrl = "https://api.github.com/repos/$repository/releases/latest"
$downloadUrl = "https://github.com/$repository/releases/download"

# 指定程序文件名和下载目录
$programFileName = "MementoMori.WebUI.exe"
$downloadDirectory = $PSScriptRoot

# 删除上次的latest.zip和解压的临时目录（如果存在）
$lastDownloadedZip = Join-Path -Path $downloadDirectory -ChildPath "latest.zip"
$lastTempDir = Join-Path -Path $downloadDirectory -ChildPath "temp"

if (Test-Path -Path $lastDownloadedZip) {
    Remove-Item -Path $lastDownloadedZip -Force
}

if (Test-Path -Path $lastTempDir) {
    Remove-Item -Path $lastTempDir -Force -Recurse
}

# 获取最新的发布信息
$latestRelease = Invoke-RestMethod -Uri $releaseApiUrl

# 提取最新版本号
$latestVersionStr = $latestRelease.tag_name

$latestVersion = [System.Version]::Parse($latestVersionStr.TrimStart("v"))

# 检查当前版本是否与最新版本相同
$currentVersion = (Get-ItemProperty -Path (Join-Path -Path $downloadDirectory -ChildPath $programFileName)).VersionInfo.FileVersionRaw

if ($currentVersion -ge $latestVersion) {
    Write-Host "程序已经是最新版本 ($latestVersion). 无需更新."
}
else {
    # 下载最新版本的压缩包
    $downloadLink = "$downloadUrl/$latestVersionStr/publish-win-x64.zip"
    $downloadPath = Join-Path -Path $downloadDirectory -ChildPath "latest.zip"
    Invoke-WebRequest -Uri $downloadLink -OutFile $downloadPath

    # 解压缩文件到临时文件夹
    $tempDir = Join-Path -Path $downloadDirectory -ChildPath "temp"
    Expand-Archive -Path $downloadPath -DestinationPath $tempDir -Force

    # 停止当前运行的程序
    Stop-Process -Name "MementoMori.WebUI" -ErrorAction SilentlyContinue

    # 复制解压后的文件到程序文件夹
    Copy-Item -Path (Join-Path -Path $tempDir -ChildPath "publish-win-x64\*") -Destination $downloadDirectory -Recurse -Force

    # 启动新程序
    Start-Process -FilePath (Join-Path -Path $downloadDirectory -ChildPath $programFileName)

    Write-Host "程序已成功更新到版本 $latestVersion."
}
