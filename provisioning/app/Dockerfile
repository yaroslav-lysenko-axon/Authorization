FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy everything and build
COPY . ./
RUN dotnet publish -c Release -o out -r linux-x64 -p:PublishSingleFile=true --self-contained true Authorization.Host

# Create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out ./

CMD ["/app/Authorization.Host"]
