# Authentication Project with C# and SQL Server in Docker

This project is an authentication application developed in C# that uses SQL Server as the database. The application and database are hosted in Docker containers to facilitate deployment and management.

## Requirements

- Docker
- Docker Compose
- Docker Desktop (for Windows)

## Project Structure

```plaintext
├── docker
│   └── dev
│       └── docker-compose.yml
├── src
│   └── modules
│   └── Main.cs
└── readme.md
```

## Project Setup

Make sure you have Docker and Docker Compose installed on your machine before proceeding, if you are on windows you can install docker using the [Docker Desktop for Windows](https://docs.docker.com/docker-for-windows/install/).

### Frontend/Client

In addition to this backend, there is a frontend/client available. You can find it at the following GitHub URL: [Frontend Repository](https://github.com/netby/netby-frontend).


## Setting Up the Development Environment

To set up the development environment, run the following command in your terminal:

```bash
docker-compose -f ./docker/dev/docker-compose.yml up
```

This command will build and bring up the necessary Docker containers for the application and the database.

## Useful Commands

### Restore the dependencies

```bash
dotnet restore
```

### Run the Application

```bash
dotnet run
```

## License

This project is licensed under a proprietary license. For more details, please contact the project maintainers.
