FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY StellarAnchor/StellarAnchor.csproj StellarAnchor/
RUN dotnet restore StellarAnchor/StellarAnchor.csproj
COPY . .
WORKDIR /src/StellarAnchor
RUN dotnet build StellarAnchor.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish StellarAnchor.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StellarAnchor.dll"]
