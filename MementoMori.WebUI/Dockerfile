#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MementoMori.WebUI/MementoMori.WebUI.csproj", "MementoMori.WebUI/"]
COPY ["MementoMori.Common/MementoMori.Common.csproj", "MementoMori.Common/"]
COPY ["MementoMori/MementoMori.csproj", "MementoMori/"]
COPY ["MementoMori.Ortega/MementoMori.Ortega.csproj", "MementoMori.Ortega/"]
RUN dotnet restore "MementoMori.WebUI/MementoMori.WebUI.csproj"
COPY . .
WORKDIR "/src/MementoMori.WebUI"
RUN dotnet build "MementoMori.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MementoMori.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MementoMori.WebUI.dll"]