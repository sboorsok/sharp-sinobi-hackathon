using sharp_sinobi_hackaton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using sharp_sinobi_hackaton.Enums;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;
<<<<<<< HEAD
=======
using System.Runtime.ConstrainedExecution;
>>>>>>> e807404b85803bf0728d79a612197785510cdb1f

// ...

namespace sharp_sinobi_hackaton
{
    public class App
    {
        private readonly DataBase database;
        public App() { }
        public App(DataBase database)
        {
            this.database = database;
        }

        public async Task Start()
        {
            Console.WriteLine("Менеджер задач");
            Console.WriteLine("---------------------");
            Console.WriteLine("1. Добавить новую задачу");
            Console.WriteLine("2. Показать все задачи");
            Console.WriteLine("3. Изменить задачу");
            Console.WriteLine("4. Отсортировать задачи");
            Console.WriteLine("5. Изменить статус задачи");
            Console.WriteLine("6. Показать задачи за период");
            Console.WriteLine("0. Выход");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.Write("Выберите действие: ");
                var key = Console.ReadKey().Key;
                Console.WriteLine();

                switch (key)
                {
                    case ConsoleKey.D1:
                        await CreateTasksObject(); // нужно вставить свой метод
                        break;

                    case ConsoleKey.D2:
                        await GetAllTaskConsole(); // показать все задачи
                        break;

                /*    case ConsoleKey.D2:
                        await GetCar();  // нужно вставить свой метод
                        break; */

                    case ConsoleKey.D3:
                        await EditTaskConsole();  // нужно вставить свой метод
                        break;
                    case ConsoleKey.D4:
                        await UpdateCar();  // нужно вставить свой метод
                        break;
                    case ConsoleKey.D5:
                        await DeleteCar();  // нужно вставить свой метод
                        break;
                    case ConsoleKey.D6:
                        await DeleteTaskConsole();  // нужно вставить свой метод
                        break;
                    case ConsoleKey.D0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверная команда. Попробуйте снова.");
                        break;
                }
            }
        }

        public async Task CreateTasksObject()  //([Name], [Description], [Date], [Priority], [Status])
        {
            Console.WriteLine("Напишите название вашей задачи: ");
            string task_name = Console.ReadLine();
            Console.WriteLine("Опишите вашу задачу");
            string task_description = Console.ReadLine();
            Console.WriteLine("Добавьте дату выполнения в формате: dd/MM/yyyy HH:mm");
            string dateTime = Console.ReadLine(); //"28/06/2023 10:30:00"
            DateTime task_date = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            Console.WriteLine("Выберите приоритетность задачи: 1 = Низкий, 2 = Средний, 3 = Высокий");

            var task_priority = (Priority)Convert.ToInt32(Console.ReadLine());

            var task_status = Status.InProcess; // дефолтное состояние статуса

            var newTask = new Tasks
            {
                Name = task_name,
                Description = task_description,
                Date = task_date,
                Priority = task_priority,
                Status = task_status,
            };

            await database.CreateNewTask(newTask);

            Console.WriteLine("Задача успешно добавлена.");

        }





        public async Task ChangeStatusConsole()
        {
            Console.WriteLine("Выберите ID задачи: ");
            int id = int.Parse(Console.ReadLine());
            var task_1 = await database.GetTaskFromDatabase(id);
            Console.WriteLine("Поменяйте статус: 1 = Выполнено, 2 = В процессе, 3 = Просрочено, 4 = Отложено ");
            int status = int.Parse(Console.ReadLine());
            switch (status)
            {
                case 1: 
                    task_1.Status = Status.Done; break;
                case 2:

                    task_1.Status = Status.InProcess; break;
                case 3:
                    task_1.Status = Status.Overdue; break;
                case 4:
                    task_1.Status = Status.Postponed; break;
                default:
                    task_1.Status = task_1.Status; break;
            }
            await database.ChangeStatus(task_1);
        }

        public async Task ConvertToJson()
        {
            Console.WriteLine("Конвертируем все задачи из базы данных в JSON");
            Thread.Sleep(2000); // имитиация долгой работы
            Console.WriteLine("Подожите пару секунд");
            Thread.Sleep(2000); // имитиация долгой работы
            var tasks = await database.GetAllTasks();
            string json = JsonConvert.SerializeObject(tasks);
            string filePath = "C:\\Users\\Боорсок\\Documents\\GitHub\\sharp-sinobi-hackathon\\json.txt"; // УКАЖИТЕ СВОИ ПУТИ К 
                                      // К ВАШЕЙ ПАПКЕ ГИТХАБА
                                      // ЧТОБЫ ВСЕ ЗАРАБОТАЛО!!
            File.WriteAllText(filePath, json, Encoding.UTF8);
            Console.WriteLine("Конвертация прошла успешно!");
        }

        public async Task GetAllTaskConsole()
        {
            Console.Clear();
            var tasks = await database.GetAllTasks();
            foreach ( var task in tasks )
            {
                Console.WriteLine($"Название: {task.Name},  Описание: {task.Description},   Дедлайн: {task.Date},   Приоритет: {task.Priority},    Статус: {task.Status} ");
            }
        }

        public async Task EditTaskConsole()
        {
            Console.Clear();
            Console.WriteLine("Выберите новое название вашей задачи");
            string name = Console.ReadLine();
            Console.WriteLine("Выберите новое описание вашей задачи");
            string description = Console.ReadLine();
            Console.WriteLine("Добавьте дату выполнения в формате: dd/MM/yyyy HH:mm");
            string dateTime = Console.ReadLine(); //"28/06/2023 10:30"
            DateTime date = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            Console.WriteLine("Выберите приоритетность задачи: 1 = Низкий, 2 = Средний, 3 = Высокий");
            var priority = (Priority)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Выберите статус задачи: 1 = Выполнено, 2 = В процессе, 3 = Просрочено, 4 = Отложено");
            var status = (Status)Convert.ToInt32(Console.ReadLine());

            var changeTask = new Tasks(name, description, date, priority, status);
            await database.EditTask(changeTask);
            Console.WriteLine("Задача была изменена успешно!");
        }

        public async Task DeleteTaskConsole() // добавил вывод в консоль метод удаления задачи
        {
            Console.Clear();
            Console.WriteLine("Выберите ID задачи для удаления: ");
            var tasks = await database.GetAllTasks();
            foreach (var task in tasks)
            {
                Console.WriteLine($"Id[{task.Id}] Задача:{task.Name}, Статус: {task.Status}");
            }
            int id = int.Parse(Console.ReadLine());
            await database.DeleteTask(id);
            Console.WriteLine("Задача была удалена успешно!");
        }

        public async Task GetSortByPriorityConsole() // добавил вывод в консоль метод сортировки по приоритету 
        {
            Console.Clear();
            Console.WriteLine("Задачи сортированные по приоритету: ");
            var tasks = await database.GetSortedTasksByPriority();
            foreach (var task in tasks)
            {
                Console.WriteLine($"Название: {task.Name},  Описание: {task.Description},   Дедлайн: {task.Date},   Приоритет: {(Priority)task.Priority},    Статус: {(Status)task.Status} ");
            }
        }

        public async Task GetSortByStatusConsole()  // добавил вывод в консоль метод сортировки по статусу 
        {
            Console.Clear();
            Console.WriteLine("Задачи сортированные по статусу: ");
            var tasks = await database.GetSortedTasksByStatus();
            foreach (var task in tasks)
            {
                Console.WriteLine($"Название: {task.Name},  Описание: {task.Description},   Дедлайн: {task.Date},   Приоритет: {(Priority)task.Priority},    Статус: {(Status)task.Status} ");
            }
        }


    }
}
