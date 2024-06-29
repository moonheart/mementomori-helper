#!/bin/bash

# publish-linux-x64/
# ├── appsettings.json
# ├── MementoMori.Common.pdb
# ├── MementoMori.Ortega.pdb
# ├── MementoMori.pdb
# ├── MementoMori.WebUI
# ├── MementoMori.WebUI.pdb
# ├── updater.sh
# └── wwwroot

# 设置GitHub仓库信息
repository="moonheart/mementomori-helper"
releaseApiUrl="https://api.github.com/repos/$repository/releases/latest"
downloadUrl="https://github.com/$repository/releases/download"

# 获取语言环境
lang=$(locale | grep LANG | cut -d= -f2 | cut -d. -f1)

# ANSI转义码颜色
RED="\033[0;31m"
GREEN="\033[0;32m"
NC="\033[0m"

# 检查所需工具的可用性
required_tools=("jq" "wget" "unzip")
missing_tools=()

for tool in "${required_tools[@]}"; do
    if ! command -v "$tool" &> /dev/null; then
        missing_tools+=("$tool")
    fi
done

if [ ${#missing_tools[@]} -ne 0 ]; then
    case "$lang" in
        zh_CN|zh_SG)
            echo -e "${RED}错误：请安装以下必要的包: ${missing_tools[*]}${NC}"
            ;;
        zh_TW|zh_HK)
            echo -e "${RED}錯誤：請安裝以下必要的包: ${missing_tools[*]}${NC}"
            ;;
        ja_JP)
            echo -e "${RED}エラー：以下の必要なパッケージをインストールしてください: ${missing_tools[*]}${NC}"
            ;;
        *)
            echo -e "${RED}Error: Please install the necessary packages: ${missing_tools[*]}${NC}"
            ;;
    esac
    exit 1
fi

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

# 获取系统架构
arch=$(uname -m)
case "$arch" in
    x86_64)
        zipName="publish-linux-x64.zip"
        ;;
    armv7l)
        zipName="publish-linux-arm.zip"
        ;;
    aarch64)
        zipName="publish-linux-arm64.zip"
        ;;
    *)
        case "$lang" in
            zh_CN|zh_SG)
                echo -e "${RED}错误：不支持的系统架构 $arch.${NC}"
                ;;
            zh_TW|zh_HK)
                echo -e "${RED}錯誤：不支持的系統架構 $arch.${NC}"
                ;;
            ja_JP)
                echo -e "${RED}エラー：サポートされていないシステムアーキテクチャ $arch。${NC}"
                ;;
            *)
                echo -e "${RED}Error: Unsupported system architecture $arch.${NC}"
                ;;
        esac
        exit 1
        ;;
esac

# 获取最新的发布信息
latestRelease=$(curl -s "$releaseApiUrl")
latestVersionStr=$(echo "$latestRelease" | jq -r '.tag_name')
latestVersion=$(echo "$latestVersionStr" | sed 's/^v//')

# 检查是否成功获取最新版本信息
if [ -z "$latestRelease" ] || [ "$latestVersionStr" = "null" ]; then
    case "$lang" in
        zh_CN|zh_SG)
            echo -e "${RED}错误：无法获取最新版本,请检查网络连接是否正常.${NC}"
            ;;
        zh_TW|zh_HK)
            echo -e "${RED}錯誤：無法獲取最新版本,請檢視網路連接狀態.${NC}"
            ;;
        ja_JP)
            echo -e "${RED}エラー：最新バージョンを取得できません。ネットワーク接続を確認してください。${NC}"
            ;;
        *)
            echo -e "${RED}Error: Unable to retrieve the latest version, please check your network connection.${NC}"
            ;;
    esac
    exit 1
fi

# 检查当前版本是否与最新版本相同
currentVersion=$(strings "$downloadDirectory/$programFileName" | grep -o 'MementoMori.WebUI/[0-9]\+\.[0-9]\+\.[0-9]\+' | awk '!seen[$0]++' | sed 's/MementoMori.WebUI\///')

if [ "$(printf '%s\n' "$currentVersion" "$latestVersion" | sort -V | head -n 1)" = "$latestVersion" ]; then
    case "$lang" in
        zh_CN|zh_SG)
            echo -e "${GREEN}程序已经是最新版本 ($latestVersion). 无需更新.${NC}"
            ;;
        zh_TW|zh_HK)
            echo -e "${GREEN}程式已經是最新版本 ($latestVersion). 無需更新.${NC}"
            ;;
        ja_JP)
            echo -e "${GREEN}プログラムはすでに最新バージョンです ($latestVersion)。更新は不要です。${NC}"
            ;;
        *)
            echo -e "${GREEN}The program is already up-to-date ($latestVersion). No update is necessary.${NC}"
            ;;
    esac
else
    case "$lang" in
        zh_CN|zh_SG)
            echo -e "${GREEN}系统架构为: $arch，开始更新...${NC}"
            ;;
        zh_TW|zh_HK)
            echo -e "${GREEN}系統架構為: $arch，開始更新...${NC}"
            ;;
        ja_JP)
            echo -e "${GREEN}システムアーキテクチャ: $arch、更新を開始します...${NC}"
            ;;
        *)
            echo -e "${GREEN}System architecture: $arch, starting update...${NC}"
            ;;
    esac
    
    # 下载最新版本的压缩包
    downloadLink="$downloadUrl/$latestVersionStr/$zipName"
    downloadPath="$downloadDirectory/latest.zip"
    wget -O "$downloadPath" "$downloadLink"

    # 解压缩文件到临时文件夹
    tempDir="$downloadDirectory/temp"
    mkdir -p "$tempDir"
    unzip "$downloadPath" -d "$tempDir"

    # 停止当前运行的程序
    pkill -f "$programFileName" || true

    # 复制解压后的文件到程序文件夹
    case "$arch" in
        x86_64)
            cp -Rf "$tempDir/publish-linux-x64/"* "$downloadDirectory/"
            ;;
        armv7l)
            cp -Rf "$tempDir/publish-linux-arm/"* "$downloadDirectory/"
            ;;
        aarch64)
            cp -Rf "$tempDir/publish-linux-arm64/"* "$downloadDirectory/"
            ;;
    esac

    # 删去临时文件夹与下载的包
    rm -rf "$tempDir" "$downloadPath"

    case "$lang" in
        zh_CN|zh_SG)
            echo -e "${GREEN}程序已成功更新到 $latestVersion.${NC}"
            ;;
        zh_TW|zh_HK)
            echo -e "${GREEN}程式已成功更新到 $latestVersion.${NC}"
            ;;
        ja_JP)
            echo -e "${GREEN}プログラムはバージョン $latestVersion に正常に更新されました。${NC}"
            ;;
        *)
            echo -e "${GREEN}The program has been successfully updated to version $latestVersion.${NC}"
            ;;
    esac
fi
