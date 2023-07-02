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
