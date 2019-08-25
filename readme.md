# Techtalk.FM

This project is intended to be an good and complete example to how to use Fluent Migrator (https://fluentmigrator.github.io/) to keep your databases environments up to date.

## Getting Started

git clone https://github.com/LeaoSomogyi/Techtalk.FM.git

### Prerequisites

DotNet Core 2.2 -> https://dotnet.microsoft.com/download/dotnet-core/2.2
SQLServer, PostgreSQL or Firebird local db instances.

SQLServer Express -> https://www.microsoft.com/pt-br/sql-server/sql-server-editions-express
PostgreSQL -> https://www.postgresql.org/download/windows/
Firebird -> https://firebirdsql.org/en/firebird-3-0/


### Installing

On project solution folder, run:

```
dotnet restore
```

This will restore packages dependencies.

```
dotnet build
```

This will build the project.

## Running the tests

```
dotnet test
```

This command will run all tests on the Techtalk.FM.Test project.

## Built With

* [dotnet core](https://docs.microsoft.com/pt-br/dotnet/core/) - Web API and Class Library
* [xUnit](https://xunit.net/) - Tests framework

## Authors

* **Felipe Somogyi** - *Initial work* - [LeaoSomogyi](https://github.com/LeaoSomogyi)

See also the list of [contributors](https://github.com/LeaoSomogyi/Techtalk.FM/graphs/contributors) who participated in this project.

