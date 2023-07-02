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
using System.Runtime.ConstrainedExecution;

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
                /*    case ConsoleKey.D2:
                        await GetCar();  // нужно вставить свой метод
                        break;эх
                    case ConsoleKey.D3:
                        await AddCar();  // нужно вставить свой метод
                        break;
                    case ConsoleKey.D4:
                        await UpdateCar();  // нужно вставить свой метод
                        break;
                    case ConsoleKey.D5:
                        await DeleteCar();  // нужно вставить свой метод
                        break;
                    case ConsoleKey.D6:
                        await DeleteCar();  // нужно вставить свой метод
                        break;*/
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

    }
}
