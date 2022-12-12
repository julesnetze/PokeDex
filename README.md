# Pokedex
Pokedex ASP.Net Core Web Api

## Requirements

The project requires [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0).

## Compatible IDEs

Tested on:

- Visual Studio 2019

## Useful commands

From the terminal/shell/command line tool, use the following commands to build, test and run the application.

### Build the project

```console
$ dotnet build
```

### Run the tests

```console
$ dotnet test TestPokedex
```

### Run the application

```console
$ dotnet run --project Pokedex
```
### Run the application with Docker

```console
$ docker compose up
```

## API Endpoints

### Basic Pokemon Information

```console
$ http://localhost:5001/pokemon/<pokemon name>
```

### Translated Pokemon Description
```console
$ http://localhost:5001/pokemon/<pokemon name>?translation=true
```
