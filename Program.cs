using Microsoft.Data.Sqlite;

namespace Empleados;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Aplicar las migraciones
        ApplyMigrations();

        Application.Run(new EmpleadosForm());
    }

    static void ApplyMigrations()
    {
        string connectionString = "Data Source=../../../Data/datasource.db";

        string migrationSqlite = @"
                CREATE TABLE IF NOT EXISTS Empleados (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT,
                    Apellido TEXT,
                    DNI TEXT,
                    Edad INTEGER,
                    Casado BOOLEAN,
                    Salario DECIMAL
                );
            ";

        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(migrationSqlite, connection);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier error que ocurra al ejecutar las migraciones
            Console.WriteLine("Error executing migrations: " + ex.Message);
        }
    }
}