#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-alpine-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Leifez/Leifez.csproj", "Leifez/"]
COPY ["Leifez.Common.Web/Leifez.Common.Web.csproj", "Leifez.Common.Web/"]
COPY ["Leifez.Infrastructure/Leifez.Infrastructure.csproj", "Leifez.Infrastructure/"]
COPY ["Leifez.Application.Domain/Leifez.Application.Domain.csproj", "Leifez.Application.Domain/"]
COPY ["Leifez.Common.Mapping/Leifez.Common.Mapping.csproj", "Leifez.Common.Mapping/"]
COPY ["Leifez.DataAccess.PostgreSQL/Leifez.DataAccess.PostgreSQL.csproj", "Leifez.DataAccess.PostgreSQL/"]
COPY ["Leifez.DataAccess/Leifez.DataAccess.csproj", "Leifez.DataAccess/"]
COPY ["Leifez.Application.Service/Leifez.Application.Service.csproj", "Leifez.Application.Service/"]
RUN dotnet restore "Leifez/Leifez.csproj" -r linux-musl-arm64
COPY . .
WORKDIR "/src/Leifez"
RUN dotnet build "Leifez.csproj" -c Release -o /app/build -r linux-musl-arm64

FROM build AS publish
RUN dotnet publish "Leifez.csproj" -c Release -o /app/publish -r linux-musl-arm64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Leifez.dll"]