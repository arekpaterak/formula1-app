Przykładowe wywołanie generatora kodu
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

dotnet aspnet-codegenerator controller -name UserController -m UserModel -dc Formula1.Data.Formula1Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries