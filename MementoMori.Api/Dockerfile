# 一个镜像 = API + 前端 (web/ 在构建时打包进 wwwroot), 一个端口
# docker build -t mementomori-helper -f MementoMori.Api/Dockerfile .

FROM node:22-alpine AS web
WORKDIR /web
RUN corepack enable && corepack prepare pnpm@9.15.0 --activate
COPY web/package.json web/pnpm-lock.yaml ./
RUN pnpm install --frozen-lockfile
COPY web/ ./
# VITE_API_URL 为空 = 和 API 同一个地址 (.env.production)
RUN pnpm exec tsc -b && pnpm exec vite build

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY global.json ./
COPY MementoMori.Common/ MementoMori.Common/
COPY MementoMori.Ortega/ MementoMori.Ortega/
COPY MementoMori.Api/ MementoMori.Api/
COPY --from=web /web/dist MementoMori.Api/wwwroot/
RUN dotnet publish MementoMori.Api -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
COPY --from=build /app /app
# 数据 (sqlite, Master, 图片缓存) 用相对路径, 放在挂载的 /data
WORKDIR /data
ENV ASPNETCORE_CONTENTROOT=/app ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "/app/MementoMori.Api.dll"]
