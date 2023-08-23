FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY *.sln .
COPY ../Wardrobe.API/*.csproj Wardrobe.API/
COPY Wardrobe.Application/*.csproj Wardrobe.Application/
COPY Wardrobe.CrossCutting/*.csproj Wardrobe.CrossCutting/
COPY Wardrobe.Domain/*.csproj Wardrobe.Domain/
COPY Wardrobe.Infra/*.csproj Wardrobe.Infra/
COPY Wardrobe.Tests/*.csproj Wardrobe.Tests/
COPY Wardrobe.BackgroundRemovalService/*.csproj Wardrobe.BackgroundRemovalService/
COPY Wardrobe.ImageClassificationService/*.csproj Wardrobe.ImageClassificationService/

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
