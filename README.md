## Creation
dotnet new webapi -o hoot-api-people --no-https -f net7.0

## Execution
dotnet run

## Create docker image
docker build -t hoot-api-people .

## Run docker image
docker run --name hoot-api-people-container -p 5000:80 hoot-api-people 
