using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharp_sinobi_hackaton.Models;
using Dapper;

namespace sharp_sinobi_hackaton
{
    public class DataBase
    {
        private readonly string connectionString; // Храним ссылку на БД в неизменяемой форме
        public DataBase() { }
        public DataBase(string connectionStrig)  // для подключения к БД мы создаем экземпляр этого класса, куда передаем путь
        {
            this.connectionString = connectionStrig;
        }

        public async Task CreateNewTask(Tasks task_1)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var sql = $@"
                        INSERT INTO Tasks 
                        ([Name], [Description], [Date], [Priority], [Status])
                        VALUES 
                        (N'{task_1.Name}', N'{task_1.Description}', {task_1.Date}, {task_1.Priority}, {task_1.Status})
                        ";
            var tec_1 = await connection.ExecuteAsync(sql);
        } // Создаем Задачу в бд, принимая объект класса Tasks

        public async Task DeleteTask(long id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "DELETE FROM ToDoBase WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task ChangeStatus(long id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "DELETE FROM ToDoBase WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }


    }
}
