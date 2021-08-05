# Reference Authorization Microservice
[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)  
This microservice is an example of **OAuth2 authorizatoin** microservice.

## Prerequisites
For a **"docker-way"**, you need only [Docker](https://www.docker.com/).

For a **"non-docker-way"**, you should have [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) installed

## How to run it?
#### Docker-way:
1. Setup a containerized *PostgreSQL*, following instructions in [db provisioning](provisioning/postgres/README.md)
3. Setup an *application*, following instructions in [app provisioning](provisioning/app/README.md)

#### Non-docker-way:
1. Setup a [PostgreSQL](https://lmgtfy.com/?q=how+to+setup+postgresql+on+linux)
3. Launch *migrations* by running this command:
    ```
    dotnet run --project Authorization.Migrations
    ```
4. Launch an *application* by running this command:
    ```
    dotnet run --project Authorization.Host
    ```

## Supported OAuth2 flows
As for now it supports only one flow - *Resource Owner Password Credential Grant Type Flow*.

![image](https://docs.oracle.com/cd/E39820_01/doc.11121/gateway_docs/content/images/oauth/oauth_username_password_flow.png)

