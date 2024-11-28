#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
RUN apt-get update && apt-get install -y wget && \
	wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
	dpkg -i packages-microsoft-prod.deb && \
	rm packages-microsoft-prod.deb && \
	apt-get update && \
	apt-get install -y dotnet-runtime-6.0 && \
	apt-get clean && \
	rm -rf /var/lib/apt/lists/* && \
	rm -rf /tmp/* && \
	rm -rf /var/tmp/* && \
	rm -rf /var/cache/apt/archives/*

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MementoMori.AssetDownloader/MementoMori.AssetDownloader.csproj", "MementoMori.AssetDownloader/"]
COPY ["MementoMori.Common/MementoMori.Common.csproj", "MementoMori.Common/"]
COPY ["MementoMori.Ortega/MementoMori.Ortega.csproj", "MementoMori.Ortega/"]
COPY ["MementoMori/MementoMori.csproj", "MementoMori/"]
RUN dotnet restore "MementoMori.AssetDownloader/MementoMori.AssetDownloader.csproj"
COPY . .
WORKDIR "/src/MementoMori.AssetDownloader"
RUN dotnet build "MementoMori.AssetDownloader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MementoMori.AssetDownloader.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "/app/MementoMori.AssetDownloader.dll"]