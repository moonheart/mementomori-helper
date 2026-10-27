#!/usr/bin/env bash
# 打包前端 → 放进 API 的 wwwroot → 同步到 Oracle → 在服务器上构建镜像并重启
# 用法: ./deploy.sh   (需要 ~/mementomori-helper-web 和 ssh ubuntu@157.137.213.122)
set -euo pipefail
HOST=ubuntu@157.137.213.122
WEB=~/mementomori-helper-web
cd "$(dirname "$0")"

(cd "$WEB" && ./node_modules/.bin/tsc -b && ./node_modules/.bin/vite build)
rm -rf MementoMori.Api/wwwroot && cp -r "$WEB/dist" MementoMori.Api/wwwroot

rsync -az --delete --exclude bin --exclude obj --exclude /MementoMori.Api/AssetCache --exclude /MementoMori.Api/Master --exclude '/MementoMori.Api/*.db' --exclude .git \
  global.json MementoMori.Common MementoMori.Ortega MementoMori.Api docker-compose.yml "$HOST:mementomori-v2/src/"
ssh "$HOST" 'cd mementomori-v2/src && sudo docker build -t mementomori-api:latest -f MementoMori.Api/Dockerfile . && cd .. && sudo docker compose -f src/docker-compose.yml up -d'
