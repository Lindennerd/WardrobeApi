FROM mcr.microsoft.com/dotnet/aspnet:7.0 
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY "../WardrobeApp.sln" "./WardrobeApp.sln"
COPY "./Wardrobe.API/Wardrobe.API.csproj" "./Wardrobe.API/Wardrobe.API.csproj"
COPY "./Wardrobe.Application/Wardrobe.Application.csproj" "./Wardrobe.Application/Wardrobe.Application.csproj"
COPY "./Wardrobe.CrossCutting/Wardrobe.CrossCutting.csproj" "./Wardrobe.CrossCutting/Wardrobe.CrossCutting.csproj"
COPY "./Wardrobe.Domain/Wardrobe.Domain.csproj" "./Wardrobe.Domain/Wardrobe.Domain.csproj"
COPY "./Wardrobe.Infra/Wardrobe.Infra.csproj" "./Wardrobe.Infra/Wardrobe.Infra.csproj"
COPY "./Wardrobe.BackgroundRemovalService/Wardrobe.BackgroundRemovalService.csproj" "./Wardrobe.BackgroundRemovalService/Wardrobe.BackgroundRemovalService.csproj"
COPY "./Wardrobe.ImageClassificationService/Wardrobe.ImageClassificationService.csproj" "./Wardrobe.ImageClassificationService/Wardrobe.ImageClassificationService.csproj"
COPY "./Wardrobe.Tests/Wardrobe.Tests.csproj" "./Wardrobe.Tests/Wardrobe.Tests.csproj"

RUN dotnet restore 

COPY . .
WORKDIR "/src/Wardrobe.API"
RUN dotnet build "Wardrobe.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wardrobe.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wardrobe.API.dll"]
