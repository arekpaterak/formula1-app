# A Formula 1 dashboard

## Authors
- Arkadiusz Paterak
- Mateusz Piękoś

## Description
The website shows the data about the current (2023) season of Formula 1.

Presented data:
- the races calendar
- results of the races
- standings of the drivers and constructors championships
- information about the drivers and teams
- information about the circuits

The website allows only logged users to view and edit the data.

The admin can manage users (primarly add new ones).

The passwords are hashed using MD5.

## Usage
To launch the website, add these tools and packages to the Formula 1 project:
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

Run the following commands:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Open the browser and go to `localhost:XXXX` with the "XXXX" as in the console.