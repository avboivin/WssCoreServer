FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY CoreServer.sln ./
COPY CoreServer/CoreServer.csproj CoreServer/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/CoreServer
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoreServer.dll"]
