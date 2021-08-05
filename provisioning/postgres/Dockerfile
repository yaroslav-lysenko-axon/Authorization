FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app

COPY . .
RUN dotnet publish -c Release -o out Authorization.Migrations

ENTRYPOINT [ "dotnet", "/app/out/Authorization.Migrations.dll" ]
