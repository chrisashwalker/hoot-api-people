# Hoot People API
The people management service for [Hoot](https://github.com/chrisashwalker/hoot) - a tiny Human Resources management system built upon microservices. 

## Run
```
dotnet run
```

## Hot Reload
```
dotnet watch
```

## Build
```
dotnet build
```

## Build Docker image
```
docker build -t hoot-api-people .
```

## Create and run docker container
```
docker run --name hoot-api-people-container -p 8001:8001 hoot-api-people 
```

## Scaffolded with
```
dotnet new webapi -o hoot-api-people --no-https -f net7.0
```
