#!/usr/bin/env bash
# 同步源码到服务器 → 在服务器上构建镜像 (前端在镜像里打包) 并重启
# 用法: ./deploy.sh   (服务器地址 user@host 写在 .deploy-host (不进 git) 或 DEPLOY_HOST 环境变量)
set -euo pipefail
cd "$(dirname "$0")"
HOST=${DEPLOY_HOST:-$(cat .deploy-host 2>/dev/null)}
[ -n "$HOST" ] || { echo "set DEPLOY_HOST=user@host or write it to .deploy-host"; exit 1; }

rsync -az --delete --exclude bin --exclude obj --exclude node_modules --exclude /web/dist --exclude /MementoMori.Api/wwwroot --exclude /MementoMori.Api/AssetCache --exclude /MementoMori.Api/Master --exclude '/MementoMori.Api/*.db' --exclude .git \
  global.json MementoMori.Common MementoMori.Ortega MementoMori.Api web docker-compose.yml "$HOST:mementomori-v2/src/"
ssh "$HOST" 'cd mementomori-v2/src && sudo docker build -q -t mementomori-api:latest -f MementoMori.Api/Dockerfile . && sudo docker compose -p mementomori-v2 up -d --force-recreate'
