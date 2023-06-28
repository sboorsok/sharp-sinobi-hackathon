using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharp_sinobi_hackaton.Enums;

namespace sharp_sinobi_hackaton.Models
{
    public class Tasks
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Prority Priority { get; set; }
        public Status Status { get; set; }


        public Tasks()
        {

        }
        public Tasks(string name, string description, DateTime date, bool priority, Status status)
        {
            Name = name;
            Description = description;
            Date = date;
            Priority = priority;
            Status = status;
        }
    }


}
