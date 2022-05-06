#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NordCloud.Services.CaseStudy/NordCloud.Services.CaseStudy.csproj", "NordCloud.Services.CaseStudy/"]
RUN dotnet restore "NordCloud.Services.CaseStudy/NordCloud.Services.CaseStudy.csproj"
COPY . .
WORKDIR "/src/NordCloud.Services.CaseStudy"
RUN dotnet build "NordCloud.Services.CaseStudy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NordCloud.Services.CaseStudy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NordCloud.Services.CaseStudy.dll"]