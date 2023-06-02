using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Formula1.Data;
using Formula1.Models;

HashAlgorithm hashAlgorithm = MD5.Create();

string password = "1234";
byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
byte[] hashedPasswordBytes = hashAlgorithm.ComputeHash(passwordBytes);
string hashedPassword = "";
foreach (byte b in hashedPasswordBytes)
{
    hashedPassword += b.ToString("X2");
}

// var connectionStringBuilder = new SqliteConnectionStringBuilder();
// connectionStringBuilder.DataSource = "./app.db";
// using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
// {
//     connection.Open();

//     SqliteCommand command = connection.CreateCommand();

//     command.CommandText = "DROP TABLE IF EXISTS logins;";
//     command.ExecuteNonQuery();

//     // command.CommandText = "DROP TABLE IF EXISTS data;";
//     // command.ExecuteNonQuery();

//     command.CommandText = "CREATE TABLE logins (\n" +
//         "    login TEXT PRIMARY KEY,\n" +
//         "    password TEXT NOT NULL\n" +
//         ");";
//     command.ExecuteNonQuery();

//     // command.CommandText = "CREATE TABLE data (\n" +
//     //     "    id INTEGER PRIMARY KEY,\n" +
//     //     "    data TEXT NOT NULL\n" +
//     //     ");";
//     // command.ExecuteNonQuery();

//     command.CommandText = $"INSERT INTO logins (login, password) VALUES ('admin', '{hashedPassword}');";
//     command.ExecuteNonQuery();

//     // command.CommandText = "INSERT INTO data (data) VALUES ('Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla euismod, nunc sed ultricies ultricies, diam nisl ultricies nisl, vitae ultricies nisl nisl eget nisl.'), ('Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla euismod, nunc sed ultricies ultricies, diam nisl ultricies nisl, vitae ultricies nisl nisl eget nisl.');";
//     // command.ExecuteNonQuery();

//     connection.Close();
// }

(List<List<string>>, string[]) ReadCSV(string filename, char separator)
{
    List<List<string>> data = new List<List<string>>();

    string[] columnNames;
    using (StreamReader reader = new StreamReader(filename))
    {
        string? header = reader.ReadLine();
        columnNames = header.Split(separator);

        while (!reader.EndOfStream)
        {
            string? line = reader.ReadLine();
            string[] values = line.Split(separator);

            List<string> row = new List<string>();
            for (int i = 0; i < values.Length; i++)
            {
                row.Add(values[i]);
            }
            data.Add(row);
        }
    }

    return (data, columnNames);
}


var connectionStringBuilder2 = new SqliteConnectionStringBuilder();
connectionStringBuilder2.DataSource = "./Formula1.db";
using (var connection = new SqliteConnection(connectionStringBuilder2.ConnectionString))
{
    connection.Open();

    SqliteCommand command = connection.CreateCommand();

    command.CommandText = "DELETE FROM UserModel;";
    command.ExecuteNonQuery();

    command.CommandText = $"INSERT INTO UserModel (Username, Password) VALUES ('admin', '{hashedPassword}');";
    command.ExecuteNonQuery();

    command.CommandText = "DELETE FROM CircuitModel;";
    command.ExecuteNonQuery();

    (List<List<string>> data, string[] columnNames) = ReadCSV("Dataset/final/circuits.csv", ',');
    for (int i = 0; i < data.Count; i++)
    {
        command.CommandText = $"INSERT INTO CircuitModel (CircuitId, Name, City, Country, Url) VALUES ({data[i][0]}, '{data[i][1]}', '{data[i][2]}', '{data[i][3]}', '{data[i][4]}');";
        command.ExecuteNonQuery();
    }

    command.CommandText = "DELETE FROM TeamModel;";
    command.ExecuteNonQuery();

    (data, columnNames) = ReadCSV("Dataset/final/teams.csv", ',');
    for (int i = 0; i < data.Count; i++)
    {
        command.CommandText = $"INSERT INTO TeamModel (TeamId, Name, Nationality, Url) VALUES ({data[i][0]}, '{data[i][1]}', '{data[i][2]}', '{data[i][3]}');";
        command.ExecuteNonQuery();
    }

    command.CommandText = "DELETE FROM DriverModel;";
    command.ExecuteNonQuery();

    (data, columnNames) = ReadCSV("Dataset/final/drivers.csv", ',');
    for (int i = 0; i < data.Count; i++)
    {
        command.CommandText = $"INSERT INTO DriverModel (DriverId, Number, Code, FirstName, LastName, DateOfBirth, Nationality, Url) VALUES ({data[i][0]}, '{data[i][1]}', '{data[i][2]}', '{data[i][3]}', '{data[i][4]}', '{data[i][5]}', '{data[i][6]}', '{data[i][7]}');";
        command.ExecuteNonQuery();
    }

    command.CommandText = "DELETE FROM RaceModel;";
    command.ExecuteNonQuery();

    (data, columnNames) = ReadCSV("Dataset/final/races.csv", ',');
    for (int i = 0; i < data.Count; i++)
    {
        command.CommandText = $"INSERT INTO RaceModel (RaceId, Year, Round, CircuitId, Name, Date, Url) VALUES ({data[i][0]}, '{data[i][1]}', '{data[i][2]}', '{data[i][3]}', '{data[i][4]}', '{data[i][5]}', '{data[i][6]}');";
        command.ExecuteNonQuery();
    }

    command.CommandText = "DELETE FROM RaceResultsModel;";
    command.ExecuteNonQuery();

    (data, columnNames) = ReadCSV("Dataset/final/race_results.csv", ',');
    for (int i = 0; i < data.Count; i++)
    {
        command.CommandText = $"INSERT INTO RaceResultsModel (RaceId, DriverId, TeamId, Position, StartingGrid, Laps, Time, Points, Sprint, SetFastestLap, FastestLapTime) VALUES ({data[i][0]}, '{data[i][1]}', '{data[i][2]}', '{data[i][3]}', '{data[i][4]}', '{data[i][5]}', '{data[i][6]}', '{data[i][7]}', '{data[i][8]}', '{data[i][9]}', '{data[i][10]}');";
        command.ExecuteNonQuery();
    }

    (data, columnNames) = ReadCSV("Dataset/final/sprint_results.csv", ',');
    for (int i = 0; i < data.Count; i++)
    {
        command.CommandText = $"INSERT INTO RaceResultsModel (RaceId, DriverId, TeamId, Position, StartingGrid, Laps, Time, Points, Sprint) VALUES ({data[i][0]}, '{data[i][1]}', '{data[i][2]}', '{data[i][3]}', '{data[i][4]}', '{data[i][5]}', '{data[i][6]}', '{data[i][7]}', '{data[i][8]}');";
        command.ExecuteNonQuery();
    }

    connection.Close();
}
// Comment ^^^

var builder = WebApplication.CreateBuilder(args);

// Dodanie obsługi stron Razor
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Formula1Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Formula1Context") ?? throw new InvalidOperationException("Connection string 'Formula1Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dodanie obsługi sesji
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10 * 60);
    options.Cookie.HttpOnly = true; //plik cookie jest niedostępny przez skrypt po stronie klienta
    options.Cookie.IsEssential = true; //pliki cookie sesji będą zapisywane dzięki czemu sesje będzie mogła być śledzona podczas nawigacji lub przeładowania strony
});
// END

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); // Dodanie obsługi sesji

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Dodanie obsługi stron Razor
app.MapRazorPages();

// Przekierowanie na stronę logowania, jeśli zasób nie istnieje
app.UseStatusCodePagesWithReExecute("/Account/Login", "?statusCode={0}"); // Przekierowanie na stronę logowania, jeśli zasób nie istnieje


// Endpoints
//endpoint metody GET, przesyła listę wszystkich obiektów Informacja
app.MapGet("/api/races", async (Formula1Context db) =>
    await db.RaceModel.ToListAsync());

//endpoint metody GET, przesyła listę wszystkich obiektów Informacja, które są priorytetowe
//(pole Priorytetowa ma wartość true)
// app.MapGet("/informacje/priorytetowa", async (Formula1Context db) =>
//     await db.Informacje.Where(t => t.Priorytetowa).ToListAsync());

//endpoint metody GET, pobiera obiekt Informacja o wybranym id
app.MapGet("/api/races/id", async (int id, Formula1Context db) =>
    await db.RaceModel.FindAsync(id)
        is RaceModel race
            ? Results.Ok(race)
            : Results.NotFound());

//endpoint metody POST, dodaje obiekt Informacja, pole klucza głównego (id) ma autoinkrement
app.MapPost("/api/races", async (RaceModel race, Formula1Context db) =>
{
    db.RaceModel.Add(race);
    await db.SaveChangesAsync();

    return Results.Created($"/races/{race.RaceId}", race);
});

//endpoint metody PUT, modyfikuje obiekt o podanym id
app.MapPut("/api/races/{id}", async (int id, RaceModel inputRace, Formula1Context db) =>
{
    var race = await db.RaceModel.FindAsync(id);

    if (race is null) return Results.NotFound();

    race.Year = inputRace.Year;
    race.Round = inputRace.Round;
    race.Circuit = inputRace.Circuit;
    race.Name = inputRace.Name;
    race.Date = inputRace.Date;
    race.Url = inputRace.Url;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

//endpoint metody DELETE, usuwa obiekt o podanym id
app.MapDelete("/api/races/{id}", async (int id, Formula1Context db) =>
{
    if (await db.RaceModel.FindAsync(id) is RaceModel race)
    {
        db.RaceModel.Remove(race);
        await db.SaveChangesAsync();
        return Results.Ok(race);
    }

    return Results.NotFound();
});

// circuits
// Endpoint metody GET, przesyła listę wszystkich obiektów Circuit
app.MapGet("/api/circuits", async (Formula1Context db) =>
    await db.CircuitModel.ToListAsync());

// Endpoint metody GET, pobiera obiekt Circuit o wybranym id
app.MapGet("/api/circuits/{id}", async (int id, Formula1Context db) =>
    await db.CircuitModel.FindAsync(id)
        is CircuitModel circuit
            ? Results.Ok(circuit)
            : Results.NotFound());

// Endpoint metody POST, dodaje obiekt Circuit, pole klucza głównego (id) ma autoinkrement
app.MapPost("/api/circuits", async (CircuitModel circuit, Formula1Context db) =>
{
    db.CircuitModel.Add(circuit);
    await db.SaveChangesAsync();

    return Results.Created($"/circuits/{circuit.CircuitId}", circuit);
});

// Endpoint metody PUT, modyfikuje obiekt o podanym id
app.MapPut("/api/circuits/{id}", async (int id, CircuitModel inputCircuit, Formula1Context db) =>
{
    var circuit = await db.CircuitModel.FindAsync(id);

    if (circuit is null) return Results.NotFound();

    circuit.Name = inputCircuit.Name;
    circuit.City = inputCircuit.City;
    circuit.Country = inputCircuit.Country;
    circuit.Url = inputCircuit.Url;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Endpoint metody DELETE, usuwa obiekt o podanym id
app.MapDelete("/api/circuits/{id}", async (int id, Formula1Context db) =>
{
    if (await db.CircuitModel.FindAsync(id) is CircuitModel circuit)
    {
        db.CircuitModel.Remove(circuit);
        await db.SaveChangesAsync();
        return Results.Ok(circuit);
    }

    return Results.NotFound();
});

// teams
// Endpoint metody GET, przesyła listę wszystkich obiektów Team
app.MapGet("/api/teams", async (Formula1Context db) =>
    await db.TeamModel.ToListAsync());

// Endpoint metody GET, przesyła listę wszystkich obiektów Team, które są priorytetowe
//(pole Priorytetowa ma wartość true)
// app.MapGet("/teams/priorytetowa", async (Formula1Context db) =>
//     await db.Teams.Where(t => t.Priorytetowa).ToListAsync());

// Endpoint metody GET, pobiera obiekt Team o wybranym id
app.MapGet("/api/teams/{id}", async (int id, Formula1Context db) =>
    await db.TeamModel.FindAsync(id)
        is TeamModel team
            ? Results.Ok(team)
            : Results.NotFound());

// Endpoint metody POST, dodaje obiekt Team, pole klucza głównego (id) ma autoinkrement
app.MapPost("/api/teams", async (TeamModel team, Formula1Context db) =>
{
    db.TeamModel.Add(team);
    await db.SaveChangesAsync();

    return Results.Created($"/teams/{team.TeamId}", team);
});

// Endpoint metody PUT, modyfikuje obiekt o podanym id
app.MapPut("/api/teams/{id}", async (int id, TeamModel inputTeam, Formula1Context db) =>
{
    var team = await db.TeamModel.FindAsync(id);

    if (team is null) return Results.NotFound();

    team.Name = inputTeam.Name;
    team.Nationality = inputTeam.Nationality;
    team.Url = inputTeam.Url;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Endpoint metody DELETE, usuwa obiekt o podanym id
app.MapDelete("/api/teams/{id}", async (int id, Formula1Context db) =>
{
    if (await db.TeamModel.FindAsync(id) is TeamModel team)
    {
        db.TeamModel.Remove(team);
        await db.SaveChangesAsync();
        return Results.Ok(team);
    }

    return Results.NotFound();
});

// drivers
// Endpoint metody GET, przesyła listę wszystkich obiektów Driver
app.MapGet("/api/drivers", async (Formula1Context db) =>
    await db.DriverModel.ToListAsync());

// Endpoint metody GET, przesyła listę wszystkich obiektów Driver, które są priorytetowe
//(pole Priorytetowa ma wartość true)
// app.MapGet("/drivers/priorytetowa", async (Formula1Context db) =>
//     await db.Drivers.Where(t => t.Priorytetowa).ToListAsync());

// Endpoint metody GET, pobiera obiekt Driver o wybranym id
app.MapGet("/api/drivers/{id}", async (int id, Formula1Context db) =>
    await db.DriverModel.FindAsync(id)
        is DriverModel driver
            ? Results.Ok(driver)
            : Results.NotFound());

// Endpoint metody POST, dodaje obiekt Driver, pole klucza głównego (id) ma autoinkrement
app.MapPost("/api/drivers", async (DriverModel driver, Formula1Context db) =>
{
    db.DriverModel.Add(driver);
    await db.SaveChangesAsync();

    return Results.Created($"/drivers/{driver.DriverId}", driver);
});

// Endpoint metody PUT, modyfikuje obiekt o podanym id
app.MapPut("/api/drivers/{id}", async (int id, DriverModel inputDriver, Formula1Context db) =>
{
    var driver = await db.DriverModel.FindAsync(id);

    if (driver is null) return Results.NotFound();

    driver.Number = inputDriver.Number;
    driver.Code = inputDriver.Code;
    driver.FirstName = inputDriver.FirstName;
    driver.LastName = inputDriver.LastName;
    driver.DateOfBirth = inputDriver.DateOfBirth;
    driver.Nationality = inputDriver.Nationality;
    driver.Url = inputDriver.Url;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Endpoint metody DELETE, usuwa obiekt o podanym id
app.MapDelete("/api/drivers/{id}", async (int id, Formula1Context db) =>
{
    if (await db.DriverModel.FindAsync(id) is DriverModel driver)
    {
        db.DriverModel.Remove(driver);
        await db.SaveChangesAsync();
        return Results.Ok(driver);
    }

    return Results.NotFound();
});

// race results
// Endpoint metody GET, przesyła listę wszystkich obiektów RaceResults
app.MapGet("/api/race-results", async (Formula1Context db) =>
    await db.RaceResultsModel.ToListAsync());

// Endpoint metody GET, przesyła listę wszystkich obiektów RaceResults dla danego wyścigu (o podanym id wyścigu)
app.MapGet("/api/race-results/race/{raceId}", async (int raceId, Formula1Context db) =>
    await db.RaceResultsModel.Where(rr => rr.Race.RaceId == raceId).ToListAsync());

// Endpoint metody GET, przesyła listę wszystkich obiektów RaceResults dla danego kierowcy (o podanym id kierowcy)
app.MapGet("/api/race-results/driver/{driverId}", async (int driverId, Formula1Context db) =>
    await db.RaceResultsModel.Where(rr => rr.Driver.DriverId == driverId).ToListAsync());

// Endpoint metody GET, przesyła listę wszystkich obiektów RaceResults dla danego zespołu (o podanym id zespołu)
app.MapGet("/api/race-results/team/{teamId}", async (int teamId, Formula1Context db) =>
    await db.RaceResultsModel.Where(rr => rr.Team.TeamId == teamId).ToListAsync());

// Endpoint metody GET, pobiera obiekt RaceResults o wybranym id
app.MapGet("/api/race-results/{id}", async (int id, Formula1Context db) =>
    await db.RaceResultsModel.FindAsync(id)
        is RaceResultsModel raceResult
            ? Results.Ok(raceResult)
            : Results.NotFound());

// Endpoint metody POST, dodaje obiekt RaceResults, pole klucza głównego (id) ma autoinkrement
app.MapPost("/api/race-results", async (RaceResultsModel raceResult, Formula1Context db) =>
{
    db.RaceResultsModel.Add(raceResult);
    await db.SaveChangesAsync();

    return Results.Created($"/race-results/{raceResult.ResultId}", raceResult);
});

// Endpoint metody PUT, modyfikuje obiekt o podanym id
app.MapPut("/api/race-results/{id}", async (int id, RaceResultsModel inputRaceResult, Formula1Context db) =>
{
    var raceResult = await db.RaceResultsModel.FindAsync(id);

    if (raceResult is null) return Results.NotFound();

    raceResult.Race = inputRaceResult.Race;
    raceResult.Driver = inputRaceResult.Driver;
    raceResult.Team = inputRaceResult.Team;
    raceResult.Position = inputRaceResult.Position;
    raceResult.StartingGrid = inputRaceResult.StartingGrid;
    raceResult.Laps = inputRaceResult.Laps;
    raceResult.Time = inputRaceResult.Time;
    raceResult.Points = inputRaceResult.Points;
    raceResult.Sprint = inputRaceResult.Sprint;
    raceResult.SetFastestLap = inputRaceResult.SetFastestLap;
    raceResult.FastestLapTime = inputRaceResult.FastestLapTime;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Endpoint metody DELETE, usuwa obiekt o podanym id
app.MapDelete("/api/race-results/{id}", async (int id, Formula1Context db) =>
{
    if (await db.RaceResultsModel.FindAsync(id) is RaceResultsModel raceResult)
    {
        db.RaceResultsModel.Remove(raceResult);
        await db.SaveChangesAsync();
        return Results.Ok(raceResult);
    }

    return Results.NotFound();
});


app.Run();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var context = services.GetRequiredService<Formula1Context>();

//     // Usuń istniejące dane
//     context.UserModel.RemoveRange(context.UserModel);
//     context.CircuitModel.RemoveRange(context.CircuitModel);
//     context.TeamModel.RemoveRange(context.TeamModel);
//     context.DriverModel.RemoveRange(context.DriverModel);
//     context.RaceModel.RemoveRange(context.RaceModel);
//     context.RaceResultsModel.RemoveRange(context.RaceResultsModel);

//     // Zapisz zmiany w bazie danych
//     context.SaveChanges();

//     // Dodaj dane przy użyciu modeli
//     string password = "1234";
//     byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
//     byte[] hashedPasswordBytes = hashAlgorithm.ComputeHash(passwordBytes);
//     string hashedPassword = "";
//     foreach (byte b in hashedPasswordBytes)
//     {
//         hashedPassword += b.ToString("X2");
//     }

//     var adminUser = new UserModel { Username = "admin", Password = hashedPassword };
//     context.UserModel.Add(adminUser);

//     (List<List<string>> data, string[] columnNames) = ReadCSV("Dataset/final/circuits.csv", ',');
//     foreach (var row in data)
//     {
//         var circuit = new CircuitModel
//         {
//             CircuitId = int.Parse(row[0]),
//             Name = row[1],
//             City = row[2],
//             Country = row[3],
//             Url = row[4]
//         };
//         context.CircuitModel.Add(circuit);
//     }

//     // Dodawanie danych do modelu TeamModel
//     (data, columnNames) = ReadCSV("Dataset/final/teams.csv", ',');
//     foreach (var row in data)
//     {
//         var team = new TeamModel
//         {
//             TeamId = int.Parse(row[0]),
//             Name = row[1],
//             Nationality = row[2],
//             Url = row[3]
//         };
//         context.TeamModel.Add(team);
//     }

//     // Dodawanie danych do modelu DriverModel
//     (data, columnNames) = ReadCSV("Dataset/final/drivers.csv", ',');
//     foreach (var row in data)
//     {
//         var driver = new DriverModel
//         {
//             DriverId = int.Parse(row[0]),
//             Number = row[1],
//             Code = row[2],
//             FirstName = row[3],
//             LastName = row[4],
//             DateOfBirth = row[5],
//             Nationality = row[6],
//             Url = row[7]
//         };
//         context.DriverModel.Add(driver);
//     }

//     // Dodawanie danych do modelu RaceModel
//     (data, columnNames) = ReadCSV("Dataset/final/races.csv", ',');
//     foreach (var row in data)
//     {
//         var race = new RaceModel();

//         race.RaceId = int.Parse(row[0]);
//         race.Year = int.Parse(row[1]);
//         race.Round = int.Parse(row[2]);

//         // Pobierz powiązany obiekt CircuitModel na podstawie CircuitId
//         int circuitId = int.Parse(row[3]);
//         var circuit = context.CircuitModel.FirstOrDefault(c => c.CircuitId == circuitId);
//         if (circuit != null)
//         {
//             race.Circuit = circuit;
//         }

//         race.Name = row[4];
//         race.Date = row[5];
//         race.Url = row[6];

//         // Dodaj obiekt do kontekstu bazy danych
//         context.RaceModel.Add(race);
//     }

//     (data, columnNames) = ReadCSV("Dataset/final/race_results.csv", ',');
//     foreach (var row in data)
//     {
//         var raceResult = new RaceResultsModel();

//         // Pobierz identyfikatory powiązanych obiektów z danych CSV
//         int raceId = int.Parse(row[0]);
//         int driverId = int.Parse(row[1]);
//         int teamId = int.Parse(row[2]);

//         // Sprawdź, czy istnieją powiązane obiekty w bazie danych
//         var race = context.RaceModel.FirstOrDefault(r => r.RaceId == raceId);
//         var driver = context.DriverModel.FirstOrDefault(d => d.DriverId == driverId);
//         var team = context.TeamModel.FirstOrDefault(t => t.TeamId == teamId);

//         if (race != null && driver != null && team != null)
//         {
//             raceResult.Race = race;
//             raceResult.Driver = driver;
//             raceResult.Team = team;

//             // Ustaw pozostałe właściwości modelu RaceResultsModel
//             raceResult.Position = row[3];
//             raceResult.StartingGrid = int.Parse(row[4]);
//             raceResult.Laps = int.Parse(row[5]);
//             raceResult.Time = row[6];
//             raceResult.Points = int.Parse(row[7]);
//             raceResult.Sprint = bool.Parse(row[8]);
//             raceResult.SetFastestLap = row[9];
//             raceResult.FastestLapTime = row[10];

//             context.RaceResultsModel.Add(raceResult);
//         }
//     }

//     // Zapisz zmiany w bazie danych
//     context.SaveChanges();
// }