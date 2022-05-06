#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NordCloud.Services.EventCatalog/NordCloud.Services.EventCatalog.csproj", "NordCloud.Services.EventCatalog/"]
RUN dotnet restore "NordCloud.Services.EventCatalog/NordCloud.Services.EventCatalog.csproj"
COPY . .
WORKDIR "/src/NordCloud.Services.EventCatalog"
RUN dotnet build "NordCloud.Services.EventCatalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NordCloud.Services.EventCatalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NordCloud.Services.EventCatalog.dll"]