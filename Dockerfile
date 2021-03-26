##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.11-buster-slim-arm64v8 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["Leifez.csproj", "Leifez/"]
#RUN dotnet restore -r linux-arm64
#COPY . .
#WORKDIR "/src/Leifez"
#RUN dotnet build "Leifez.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Leifez.csproj" -c release -o /app -r linux-arm64 --self-contained false --no-restore
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Leifez.dll"]

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY */*.csproj /source/
RUN dotnet restore -r linux-arm64

# copy everything else and build app
COPY . /source/
WORKDIR /source/Leifez
RUN dotnet publish -c release -o /app -r linux-arm64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1.11-buster-slim-arm64v8
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./Leifez"]

RUN apt-get update && apt-get install -y libgdiplus
