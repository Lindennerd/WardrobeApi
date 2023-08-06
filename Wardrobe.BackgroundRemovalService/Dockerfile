FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Wardrobe.BackgroundRemovalService/Wardrobe.BackgroundRemovalService.csproj", "Wardrobe.BackgroundRemovalService/"]
RUN dotnet restore "Wardrobe.BackgroundRemovalService/Wardrobe.BackgroundRemovalService.csproj"
COPY . .
WORKDIR "/src/Wardrobe.BackgroundRemovalService"
RUN dotnet build "Wardrobe.BackgroundRemovalService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wardrobe.BackgroundRemovalService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wardrobe.BackgroundRemovalService.dll"]
