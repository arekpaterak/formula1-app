```
dotnet aspnet-codegenerator controller -name DriverController -m DriverModel -dc Formula1.Data.Formula1Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries -sqlite
```

```
dotnet ef migrations add InitialCreate
```

Update bazy danych dokonuje się poleceniem:
```
dotnet ef database update
```