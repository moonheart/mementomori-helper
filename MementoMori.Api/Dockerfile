# API + 打包好的前端 (MementoMori.Api/wwwroot, 由 deploy.sh 放进去), 一个端口
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY global.json ./
COPY MementoMori.Common/ MementoMori.Common/
COPY MementoMori.Ortega/ MementoMori.Ortega/
COPY MementoMori.Api/ MementoMori.Api/
RUN dotnet publish MementoMori.Api -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
COPY --from=build /app /app
# 数据 (sqlite, Master, 图片缓存) 用相对路径, 放在挂载的 /data
WORKDIR /data
ENV ASPNETCORE_CONTENTROOT=/app ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "/app/MementoMori.Api.dll"]
