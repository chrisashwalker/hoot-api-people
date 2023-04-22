## Creation
dotnet new webapi -o hoot-api-people --no-https -f net7.0

## Execution
dotnet run

## Hot Reload
dotnet watch

# Build
dotnet build

## Create docker image
docker build -t hoot-api-people .

## Run docker image
docker run --rm --name hoot-api-people-container -p 8001:8001 hoot-api-people 
