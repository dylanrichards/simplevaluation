# https://docs.docker.com/samples/dotnetcore/#method-1
# docker build -t dylantrichards/simplevaluation:latest .
# docker run -e FMF_APIKEY=<key> -p 8080:80 dylantrichards/simplevaluation

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "SimpleValuation.dll"]
