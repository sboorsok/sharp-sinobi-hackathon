using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharp_sinobi_hackaton.Models;
using Dapper;
using System.Runtime.ConstrainedExecution;

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
            var sql = "INSERT INTO Tasks (Name, Description, Date, Priority, Status) VALUES (@Name, @Description, @Date, @Priority, @Status)";
            var tec_1 = await connection.ExecuteAsync(sql, task_1);
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

        public async Task ChangeStatus(Tasks task_1)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE ToDoBase SET Priority = @Priority";
                await connection.ExecuteAsync(query, task_1);
            }
        }

        public async Task<IEnumerable<Tasks>> GetAllTasks()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM ToDoBase";
                return await connection.QueryAsync<Tasks>(query);
            }
        }

        public async Task EditTask(Tasks task_1)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE ToDoBase SET Name = @Name, Description = @Description, Date = @Date, Priority = @Priority, Status = @Status WHERE Id = @Id";
                await connection.ExecuteAsync(query, task_1);
            }
        }

        public async Task<IEnumerable<Tasks>> GetSortedTasksByDate()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM ToDoBase ORDER BY Date";
                return await connection.QueryAsync<Tasks>(query);
            }
        }

        public async Task<IEnumerable<Tasks>> GetSortedTasksByPriority()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM ToDoBase ORDER BY Priority";
                return await connection.QueryAsync<Tasks>(query);
            }
        }

        public async Task<IEnumerable<Tasks>> GetTasksByDate()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM ToDoBase WHERE BETWEEN '2023-06-01' AND '2023-06-30'";
                return await connection.QueryAsync<Tasks>(query);
            }
        }


    }
}
