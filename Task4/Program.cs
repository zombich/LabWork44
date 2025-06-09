using Microsoft.Data.SqlClient;

CreateDatabase();


void CreateDatabase()
{
    SqlConnectionStringBuilder builder = new()
    {
        DataSource = "mssql",
        InitialCatalog = "ispp3104",
        UserID = "ispp3104",
        Password = "3104",
        TrustServerCertificate = true
    };

    string connectionString = builder.ConnectionString;

    using SqlConnection connection = new(connectionString);
    connection.Open();
    Console.WriteLine("Подключено");

    string query = @"CREATE TABLE Roles (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(200) NOT NULL
                        )";
    SqlCommand command = new(query, connection);
    command.ExecuteNonQuery();

    query = @"CREATE TABLE Users (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                RoleId INT NOT NULL,
                Login NVARCHAR(50) NOT NULL,
                Password NVARCHAR(30) NOT NULL,
                FOREIGN KEY (RoleId) REFERENCES Roles(Id)
                )";
    command = new(query, connection);
    command.ExecuteNonQuery();

    Console.WriteLine("Таблицы созданы");
}