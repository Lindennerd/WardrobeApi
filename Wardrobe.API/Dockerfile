FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Wardrobe.API/Wardrobe.API.csproj", "Wardrobe.API/"]
RUN dotnet restore "Wardrobe.API/Wardrobe.API.csproj"
COPY . .
WORKDIR "/src/Wardrobe.API"
RUN dotnet build "Wardrobe.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wardrobe.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wardrobe.API.dll"]
