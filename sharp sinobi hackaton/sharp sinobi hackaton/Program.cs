using Dapper;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using sharp_sinobi_hackaton;
using sharp_sinobi_hackaton.StaticHelper;

namespace EXAM_MARK1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Server=.; Database=ToDoBase; Trusted_connection=True; Encrypt=Optional";
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var database = new DataBase(connectionString);
            var task_1 = Helper.CreateTasksObject();
            await database.CreateNewTask(task_1);



            //var app = new App(database);

            //await app.Start();


        }


    }
}




