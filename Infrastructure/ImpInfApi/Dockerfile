#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ImpInfApi/ImpInfApi.csproj", "ImpInfApi/"]
COPY ["ImpInfCommon/ImpInfCommon.csproj", "ImpInfCommon/"]
COPY ["tg-bot-lib/TgBotLib/TgBotLib.csproj", "tg-bot-lib/TgBotLib/"]
RUN dotnet restore "ImpInfApi/ImpInfApi.csproj"
COPY . .
WORKDIR "/src/ImpInfApi"
RUN dotnet build "ImpInfApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImpInfApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImpInfApi.dll"]