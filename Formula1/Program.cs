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