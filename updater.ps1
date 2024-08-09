# open powershell with admin and run Set-ExecutionPolicy RemoteSigned

$ErrorActionPreference = "Stop"

# ����GitHub�ֿ���Ϣ
$repository = "moonheart/mementomori-helper"
$releaseApiUrl = "https://api.github.com/repos/$repository/releases/latest"
$downloadUrl = "https://github.com/$repository/releases/download"

# ��ȡ���Ի���
$language = (Get-Culture).Name

# ANSI��ɫ����
$Red = "Red"
$Green = "Green"
$Yellow = "Yellow"

# ��Ϣ���뺯��
function Translate-Message($messageKey, $params = @{}) {
    $messages = @{
        "AlreadyLatest" = @{
            "zh-CN" = "�����Ѿ������°汾 ($($params.currentVersionStr)). ������¡�"
            "zh-TW" = "��ʽ�ѽ������°汾 ($($params.currentVersionStr)). �o����¡�"
            "ja-JP" = "�ץ����Ϥ��Ǥ����¥Щ`�����Ǥ� ($($params.currentVersionStr))�����¤ϲ�Ҫ�Ǥ���"
            "default" = "The program is already the latest version ($($params.currentVersionStr)). No update is necessary."
        }
        "StartUpdate" = @{
            "zh-CN" = "��ʼ����..."
            "zh-TW" = "�_ʼ����..."
            "ja-JP" = "���¤��_ʼ���ޤ�..."
            "default" = "Starting update..."
        }
        "UpdateSuccess" = @{
            "zh-CN" = "�����ѳɹ����µ��汾 $($params.latestVersionStr)."
            "zh-TW" = "��ʽ�ѳɹ����µ��汾 $($params.latestVersionStr)."
            "ja-JP" = "�ץ����ϥЩ`����� $($params.latestVersionStr) �������˸��¤���ޤ�����"
            "default" = "The program has been successfully updated to version $($params.latestVersionStr)."
        }
        "NoLocalVersion" = @{
            "zh-CN" = "δ��⵽���س���汾��������Ҫ���¡�"
            "zh-TW" = "δ�z�y�����س�ʽ�汾��������Ҫ���¡�"
            "ja-JP" = "��`����ץ����Щ`����󤬗ʳ�����ޤ���Ǥ��������¤���Ҫ���⤷��ޤ���"
            "default" = "No local program version detected, update may be required."
        }
        "LatestVersion" = @{
            "zh-CN" = "���°汾: $($params.latestVersionStr)"
            "zh-TW" = "���°汾: $($params.latestVersionStr)"
            "ja-JP" = "���¥Щ`�����: $($params.latestVersionStr)"
            "default" = "Latest Version: $($params.latestVersionStr)"
        }
        "CurrentVersion" = @{
            "zh-CN" = "��ǰ�汾: v$($params.cleanedCurrentVersionStr)"
            "zh-TW" = "��ǰ�汾: v$($params.cleanedCurrentVersionStr)"
            "ja-JP" = "�F�ڤΥЩ`�����: v$($params.cleanedCurrentVersionStr)"
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

# ָ�������ļ���������Ŀ¼
$programFileName = "MementoMori.WebUI.exe"
$downloadDirectory = $PSScriptRoot

# ɾ���ϴε�latest.zip�ͽ�ѹ����ʱĿ¼��������ڣ�
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

# ��ȡ���µķ�����Ϣ
$latestRelease = Invoke-RestMethod -Uri $releaseApiUrl
$latestVersionStr = $latestRelease.tag_name
Write-Host (Translate-Message "LatestVersion" @{ latestVersionStr = $latestVersionStr }) -ForegroundColor $Yellow

# ��ȡ���س���İ汾��Ϣ
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

# �������°汾��ѹ����
$zipName = "publish-win-x64.zip"
$downloadLink = "$downloadUrl/$latestVersionStr/$zipName"

$downloadPath = Join-Path -Path $downloadDirectory -ChildPath "latest.zip"
Invoke-WebRequest -Uri $downloadLink -OutFile $downloadPath

# ��ѹ���ļ�����ʱ�ļ���
$tempDir = Join-Path -Path $downloadDirectory -ChildPath "temp"
Expand-Archive -Path $downloadPath -DestinationPath $tempDir -Force

# ֹͣ��ǰ���еĳ���
Stop-Process -Name "MementoMori.WebUI" -ErrorAction SilentlyContinue

# ���ƽ�ѹ����ļ��������ļ���
Copy-Item -Path (Join-Path -Path $tempDir -ChildPath "publish-win-x64\*") -Destination $downloadDirectory -Recurse -Force

# ɾȥ��ʱ�ļ��������صİ�
Remove-Item -Path $tempDir -Recurse -Force
Remove-Item -Path $downloadPath -Force

# �����³���
Start-Process -FilePath (Join-Path -Path $downloadDirectory -ChildPath $programFileName)

Write-Host (Translate-Message "UpdateSuccess" @{ latestVersionStr = $latestVersionStr }) -ForegroundColor $Green
