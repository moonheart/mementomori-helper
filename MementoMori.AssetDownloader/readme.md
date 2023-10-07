# Assets Downloader

Automatically download assets from memento mori. Only `AddressableConvertAssets` parts.

## Usage

Create docker-compose.yml and .env file in the same directory.

docker-compose.yml
```yaml
version: "3"
services:
  mementomori-downloader:
    image: moonheartmoon/mementomori-downloader:v1
    container_name: mementomori-downloader
    volumes:
      - ./data:/app/data
    restart: unless-stopped
    env_file:
      - .env
```

.env

```env
GameOs=Android
AssetStutioCliPath=/app/data/assetstudio-cli/ArknightsStudioCLI
DownloadPath=/app/data/Assets/
AListUrl=
AlistUsername=
AlistPassword=
AListTargetPath=
```

Then download AssetStudioCLI from [here](https://github.com/aelurum/AssetStudio/releases) and put it in `./data/assetstudio-cli/`.

Finally, run `docker-compose up -d` to start the service.