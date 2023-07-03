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
            using (var connection = new SqlConnection(connectionString)) 
            { 
                connection.Open();
                var query = "INSERT INTO Tasks (Name, Description, Date, Priority, Status) VALUES (@Name, @Description, @Date, @Priority, @Status)";
                await connection.QueryAsync<Tasks>(query, task_1);
            }
        } // Создаем Задачу в бд, принимая объект класса Tasks

        public async Task DeleteTask(long id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Tasks WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task ChangeStatus(int taskId, int newStatus)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE Tasks SET Status = @NewStatus WHERE Id = @TaskId";
                await connection.ExecuteAsync(query, new { NewStatus = newStatus, TaskId = taskId });
            }
        }

        public async Task<IEnumerable<Tasks>> GetAllTasks()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Tasks";
                return await connection.QueryAsync<Tasks>(query);
            }
        }

        public async Task EditTask(Tasks task_1)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE Tasks SET Name = @Name, Description = @Description, Date = @Date, Priority = @Priority, Status = @Status WHERE Id = @Id";
                await connection.ExecuteAsync(query, new
                {
                    task_1.Name,
                    task_1.Description,
                    task_1.Date,
                    task_1.Priority,
                    task_1.Status,
                    task_1.Id
                });
            }
        }

        public async Task<IEnumerable<Tasks>> GetSortedTasksByDate()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Tasks ORDER BY Date";
                return await connection.QueryAsync<Tasks>(query);
            }
        }

        public async Task<IEnumerable<Tasks>> GetSortedTasksByPriority()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Tasks ORDER BY Priority";
                return await connection.QueryAsync<Tasks>(query);
            }
        }

        public async Task<IEnumerable<Tasks>> GetSortedTasksByStatus() //добавил метод сортировки по статусу
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Tasks ORDER BY Status";
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

        public async Task<Tasks> GetTaskFromDatabase(int taskId)   // это метод вспомогательный, ЕГО НЕ НУЖНО РЕАЛИЗОЫВАТЬ 
        {                                                          // в калссе АПП
            using (var connection = new SqlConnection(connectionString)) // он помогает при работе остальных методов 
            {
                connection.Open();
                var sql = "SELECT * FROM Tasks WHERE Id = @TaskId";
                var parameters = new { TaskId = taskId };
                return await connection.QueryFirstOrDefaultAsync<Tasks>(sql, parameters);
            }
        }

        public async Task<IEnumerable<Tasks>> GetSortedTasksByDate(DateTime date)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();


                var query = "SELECT * FROM Tasks Where Date = @Date ORDER BY Date";

                return await connection.QueryAsync<Tasks>(query, date);
            }
        }
    }
}
