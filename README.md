# Customer Management

A .NET 10 application for managing customer records.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- A database server (configured via connection string)

## Getting started

Clone the repository and restore dependencies:

```bash
git clone https://github.com/developerharon/CustomerManagement.git
cd CustomerManagement
dotnet restore
```

Set the connection string in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<your-connection-string>"
  }
}
```

Apply migrations (if using EF Core):

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```
