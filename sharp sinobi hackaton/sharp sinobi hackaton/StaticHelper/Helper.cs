using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharp_sinobi_hackaton.Models;

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
            Console.WriteLine("Добавьте дату выполнения");
            
            

        }
    }
}
