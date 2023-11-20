#!/bin/bash

# 1.请将脚本放在解压后的文件夹内，它的树状图应该是这样：
#
# publish-linux-x64/
# ├── appsettings.json
# ├── MementoMori.Common.pdb
# ├── MementoMori.Ortega.pdb
# ├── MementoMori.pdb
# ├── MementoMori.WebUI
# ├── MementoMori.WebUI.pdb
# ├── updater.sh
# └── wwwroot
#
# 2.确保你的系统中已经安装了 jq wget unzip 这三个工具
#
# 3.接着赋予这个脚本可执行权限
#
# 4.最后，在终端中执行它

# 设置GitHub仓库信息
repository="moonheart/mementomori-helper"
releaseApiUrl="https://api.github.com/repos/$repository/releases/latest"
downloadUrl="https://github.com/$repository/releases/download"

# 检查所需工具的可用性
required_tools=("jq" "wget" "unzip")

for tool in "${required_tools[@]}"; do
    if ! command -v "$tool" &> /dev/null; then
        echo "错误：请安装 $tool 等必要的包."
        exit 1
    fi
done

# 指定程序文件名和下载目录
programFileName="MementoMori.WebUI"
downloadDirectory="$(pwd)"

# 删除上次的latest.zip和解压的临时目录（如果存在）
lastDownloadedZip="$downloadDirectory/latest.zip"
lastTempDir="$downloadDirectory/temp"

if [ -f "$lastDownloadedZip" ]; then
    rm -rf "$lastDownloadedZip"
fi

if [ -d "$lastTempDir" ]; then
    rm -rf "$lastTempDir"
fi

# 获取最新的发布信息
latestRelease=$(curl -s "$releaseApiUrl")
latestVersionStr=$(echo "$latestRelease" | jq -r '.tag_name')
latestVersion=$(echo "$latestVersionStr" | sed 's/^v//')

# 检查是否成功获取最新版本信息
if [ -z "$latestRelease" ] || [ "$latestVersionStr" = "null" ]; then
    echo "错误：无法获取最新版本,请检查网络连接是否正常."
    exit 1
fi

# 检查当前版本是否与最新版本相同
currentVersion=$(strings "$downloadDirectory/$programFileName" | grep -o 'MementoMori.WebUI/[0-9]\+\.[0-9]\+\.[0-9]\+' | awk '!seen[$0]++' | sed 's/MementoMori.WebUI\///')

if [ "$(printf '%s\n' "$currentVersion" "$latestVersion" | sort -V | head -n 1)" = "$latestVersion" ]; then
    echo "程序已经是最新版本 ($latestVersion). 无需更新."
else

    # 下载最新版本的压缩包
    downloadLink="$downloadUrl/$latestVersionStr/publish-linux-x64.zip"
    downloadPath="$downloadDirectory/latest.zip"
    wget -O "$downloadPath" "$downloadLink"

    # 解压缩文件到临时文件夹
    tempDir="$downloadDirectory/temp"
    mkdir -p "$tempDir"
    unzip "$downloadPath" -d "$tempDir"

    # 停止当前运行的程序
    pkill -f "$programFileName" || true

    # 复制解压后的文件到程序文件夹
    cp -Rf "$tempDir/publish-linux-x64/"* "$downloadDirectory/"

    # 删去临时文件夹与下载的包
    rm -rf "$tempDir" "$downloadPath"

    echo "程序已成功更新到版本 $latestVersion."
fi
