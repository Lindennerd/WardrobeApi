FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Wardrobe.ImageClassificationService/Wardrobe.ImageClassificationService.csproj", "Wardrobe.ImageClassificationService/"]
RUN dotnet restore "Wardrobe.ImageClassificationService/Wardrobe.ImageClassificationService.csproj"
COPY . .
WORKDIR "/src/Wardrobe.ImageClassificationService"
RUN dotnet build "Wardrobe.ImageClassificationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wardrobe.ImageClassificationService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wardrobe.ImageClassificationService.dll"]
