#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim-arm64v8 AS base
WORKDIR /app
#RUN cd /usr/lib && ln -s libgdiplus.so gdiplus.dll && ln -s libhostpolicy.so /app/libhostpolicy.so 
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Leifez/Leifez.csproj", "Leifez/"]
COPY ["Leifez.Common.Configuration/Leifez.Common.Configuration.csproj", "Leifez.Common.Configuration/"]
COPY ["Leifez.Common.Web/Leifez.Common.Web.csproj", "Leifez.Common.Web/"]
COPY ["Leifez.Core.Infrastructure/Leifez.Core.Infrastructure.csproj", "Leifez.Core.Infrastructure/"]
COPY ["Leifez.Application.Domain/Leifez.Application.Domain.csproj", "Leifez.Application.Domain/"]
COPY ["Leifez.Common.Mapping/Leifez.Common.Mapping.csproj", "Leifez.Common.Mapping/"]
COPY ["Leifez.Core.PostgreSQL/Leifez.Core.PostgreSQL.csproj", "Leifez.Core.PostgreSQL/"]
COPY ["Leifez.Application.Service/Leifez.Application.Service.csproj", "Leifez.Application.Service/"]
RUN dotnet restore "Leifez/Leifez.csproj" -r linux-arm64
COPY . .
WORKDIR "/src/Leifez"
RUN ASPNETCORE_ENVIRONMENT="Production" \
	dotnet build "Leifez.csproj" -c Release -o /app/build -r linux-musl-arm64

FROM build AS publish
RUN dotnet publish "Leifez.csproj" -c Release -o /app/publish -r linux-musl-arm64

FROM base AS final
WORKDIR /app
RUN apt-get update && apt-get install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /app/libgdiplus.dll	
RUN ln -s /usr/lib64/libc.so.6 /usr/lib64/libc.musl-x86_64.so.1
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Leifez.dll"]
