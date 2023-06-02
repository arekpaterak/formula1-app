# Strona Formuły 1
## Autorzy
- Arkadiusz Paterak
- Mateusz Piękoś

## Opis
Strona internetowa prezentuje dane dotyczące aktualnego sezonu Formuły 1 (2023).

Przedstawione dane:
- kalendarz wyścigów
- wyniki wyścigów
- klasyfikacje kierowców i konstruktorów
- informacje o kierowcach i zespołach
- informacje o torach

Aby przeglądać i edytować dane, strona internetowa wymaga logowania.

Administrator ma możliwość zarządzania użytkownikami (głównie dodawanie nowych).

Hasła są hashowane przy użyciu MD5.

## Użycie
Aby uruchomić stronę internetową, dodaj następujące narzędzia i paczki do projektu Formula 1:

```
dotnet tool uninstall --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-aspnet-codegenerator

dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef

dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Uruchom następujące polecenia:

```
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Otwórz przeglądarkę i przejdź pod adres `localhost:XXXX`, gdzie "XXXX" jest portem podanym w konsoli.