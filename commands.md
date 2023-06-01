Przykładowe wywołanie generatora kodu
```
dotnet aspnet-codegenerator controller -name DriverController -m DriverModel -dc Formula1.Data.Formula1Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries -sqlite
```

dotnet aspnet-codegenerator controller -name RaceController -m RaceModel -dc Formula1.Data.Formula1Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name RaceResultsController -m RaceResultsModel -dc Formula1.Data.Formula1Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name CircuitController -m CircuitModel -dc Formula1.Data.Formula1Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name TeamController -m TeamModel -dc Formula1.Data.Formula1Context --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

-f to overwrite


```
dotnet ef migrations add InitialCreate
```

Update bazy danych dokonuje się poleceniem:
```
dotnet ef database update
```