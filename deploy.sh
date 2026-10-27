#!/usr/bin/env bash
# 打包前端 → 放进 API 的 wwwroot → 同步到服务器 → 在服务器上构建镜像并重启
# 用法: ./deploy.sh   (服务器地址 user@host 写在 .deploy-host (不进 git) 或 DEPLOY_HOST 环境变量)
set -euo pipefail
WEB=web  # 前端在同一个仓库里 (moonheart: helper-web 只是临时仓库, 要并进来)
cd "$(dirname "$0")"
HOST=${DEPLOY_HOST:-$(cat .deploy-host 2>/dev/null)}
[ -n "$HOST" ] || { echo "set DEPLOY_HOST=user@host or write it to .deploy-host"; exit 1; }

(cd "$WEB" && ./node_modules/.bin/tsc -b && ./node_modules/.bin/vite build)
rm -rf MementoMori.Api/wwwroot && cp -r "$WEB/dist" MementoMori.Api/wwwroot

rsync -az --delete --exclude bin --exclude obj --exclude /MementoMori.Api/AssetCache --exclude /MementoMori.Api/Master --exclude '/MementoMori.Api/*.db' --exclude .git \
  global.json MementoMori.Common MementoMori.Ortega MementoMori.Api docker-compose.yml "$HOST:mementomori-v2/src/"
ssh "$HOST" 'cd mementomori-v2/src && sudo docker build -q -t mementomori-api:latest -f MementoMori.Api/Dockerfile . && sudo docker compose -p mementomori-v2 up -d --force-recreate'
