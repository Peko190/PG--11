using System;
using PG_Тема_11.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Domain.Entites
{
    public class Courses
    {
        public int Id { get; private set; }
        public string Title { get;  set; }
        public string Description { get;  set; }
        public  Level level { get;  set; }

        public Courses(int id, string title, string description, Level level)
        {
            Id = id;
            Title = title;
            Description = description;
            this.level = level;
        }

    }
}
