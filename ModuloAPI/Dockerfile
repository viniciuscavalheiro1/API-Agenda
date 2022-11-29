FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ModuloAPI/ModuloAPI.csproj", "ModuloAPI/"]
RUN dotnet restore "ModuloAPI/ModuloAPI.csproj"
COPY . .
WORKDIR "/src/ModuloAPI"
RUN dotnet build "ModuloAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModuloAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModuloAPI.dll"]
