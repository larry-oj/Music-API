# docker build -t musicapi:1.0 -f .\DotNetMusicApi\Dockerfile .
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../DotNetMusicApi/DotNetMusicApi.csproj", "DotNetMusicApi/"]
COPY ["../DotNetMusicApi.Services/DotNetMusicApi.Services.csproj", "DotNetMusicApi.Services/"]
RUN dotnet restore "DotNetMusicApi/DotNetMusicApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "DotNetMusicApi/DotNetMusicApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetMusicApi/DotNetMusicApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetMusicApi.dll", "--urls", "http://0.0.0.0:5010;"]
