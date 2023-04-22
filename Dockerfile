FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY hoot-api-people.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8001
ENTRYPOINT ["dotnet", "hoot-api-people.dll"]
