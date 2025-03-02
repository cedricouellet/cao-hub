# CAOHub (cao-hub)

My module-based application suite, which I plan to grow over time.

## Documentation

### Modules

- [Receipt Management](/documentation/receipt-management.md)

## Requirements

Requirements:

- [Docker](https://www.docker.com/) 
- [Docker Compose](https://docs.docker.com/compose/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
    - [ASP.NET and web development workload](https://learn.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022)
- [.NET Core CLI - Entity Framework Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Getting Started - in only 4 simple steps!

### 0. Configure the environment if needed

Copy [.example.env](./.example.env) file and rename it to ".env".

Change the default values if you need to, and make sure to fill empty values, as they are required for the application and database to function whatsoever. 

### 1. Start the containerized MSSQL database using Docker

From the root of the repository, run the following command in a terminal: 

```
docker-compose up
```

#### Optional flags

- `-d`/`--detach`: Detach the console from the container's terminal output (recommended)

- `-b`/`--build`: Force the container images to rebuild

> If the application database and default users have not yet been initialized, it will do so at this time.

### 2. Apply REST API database migrations

Inside the [src/CaoHub.Data](./src/CaoHub.Data/) project directory, run the following command in a terminal:

```
dotnet ef database update
```

> Note: you only need to do this when starting fresh, or when new migrations have been added since you last updated the database.  

For more information on working with migrations (such as adding them), including how to revert migrations, refer to [Microsoft's documentation on the matter](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli).

### 3. Run the REST API

> If you want to change the default HTTP ports used by the web app, refer to the file located at [src/Caohub.Api/Properties/launchSettings.json](./src/CaoHub.Api/Properties/launchSettings.json).

Build and run the project located at [src/CaoHub.Api](./src/CaoHub.Api/) using Visual Studio 2022 or the `dotnet run`/`dotnet watch run` command in a terminal.

### 4. Run the React app

> TODO

