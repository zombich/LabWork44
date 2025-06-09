using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

CreateDatabase("test");


void CreateDatabase(string name)
{
    string file = Path.Combine(Environment.CurrentDirectory, $"{name}.sqlite");
    if (File.Exists(file))
    {
        Console.WriteLine("БД существует");
        return;
    }

    SqliteConnectionStringBuilder builder = new();
    builder.DataSource = file;
    string connectionString = builder.ConnectionString;

    using SqliteConnection connection = new(connectionString);
    connection.Open();
    Console.WriteLine("БД создано");

    string query = @"CREATE TABLE Game(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    PublicationYear INTEGER NOT NULL,
                    Price REAL NOT NULL
                    )";
    SqliteCommand command = new(query, connection);
    command.ExecuteNonQuery();

    query = @"CREATE TABLE Review (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            GameId INTEGER NOT NULL, User TEXT NOT NULL,
            Comment TEXT NOT NULL,
            PublicationDate TEXT NOT NULL,
            FOREIGN KEY (GameId) REFERENCES Game(Id))";
    command = new(query, connection);
    command.ExecuteNonQuery();

    Console.WriteLine("Таблицы созданы");
}