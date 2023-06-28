using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharp_sinobi_hackaton.Models;
using sharp_sinobi_hackaton.Enums;

namespace sharp_sinobi_hackaton.StaticHelper
{
    public static class Helper
    {
        public static Tasks CreateTasksObject()  //([Name], [Description], [Date], [Priority], [Status])
        {
            Console.WriteLine("Напишите название вашей задачи: ");
            string task_name = Console.ReadLine();
            Console.WriteLine("Опишите вашу задачу");
            string task_description = Console.ReadLine();
            Console.WriteLine("Добавьте дату выполнения в формате: dd/MM/yyyy HH:mm");
            string dateTime = Console.ReadLine(); //"28/06/2023 10:30:00"
            DateTime task_date = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            Console.WriteLine("Выберите приоритетность задачи: 1 = Низкий, 2 = Средний, 3 = Высокий");
            byte checker = byte.Parse(Console.ReadLine());
            Priority task_priority;
            switch (checker)
            {
                case 1:
                    task_priority = Priority.Low;
                    break;
                case 2:
                    task_priority = Priority.Middle;
                    break;
                case 3:
                    task_priority = Priority.High;
                    break;
                default:
                    task_priority = Priority.Low; // Значение по умолчанию
                    break;
            }
            var task_status = Status.InProcess; // дефолтное состояние статуса

            return new Tasks(task_name, task_description, task_date, task_priority, task_status);

        }
    }
}
