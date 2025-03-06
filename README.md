# CAOHub (cao-hub)

My module-based application suite, which I plan to grow over time.

## Documentation

### Modules

- [Receipt Management](/documentation/receipt-management.md)

## Requirements

Requirements:

- [Docker](https://www.docker.com/) 
- [Docker Compose](https://docs.docker.com/compose/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [.NET Core CLI - Entity Framework Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

## Getting Started - in only 4 simple steps!

### 0. Configure the environment if needed

Copy [.example.env](./.example.env) file and rename it to ".env".

Change the default values if you need to, and make sure to fill empty values, as they are required for the application and database to function whatsoever. 

### 1. Setting up the database

From the root of the repository, run the following command in a terminal: 

```
docker-compose up
```

#### Optional flags

- `-d`/`--detach`: Detach the console from the container's terminal output (recommended)

- `-b`/`--build`: Force the container images to rebuild

> If the application database and default users have not yet been initialized, it will do so at this time.

### 2. Settings up the backend

> TODO

### 3. Setting up the frontend

> TODO
