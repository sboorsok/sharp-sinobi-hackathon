using Dapper;
using Microsoft.Data.SqlClient;
using sharp_sinobi_hackaton;
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Server=.; Database=ToDoBase; Trusted_connection=True; Encrypt=Optional";
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var database = new DataBase(connectionString);
            var app = new App(database);
            await app.Start();
        }
    }




