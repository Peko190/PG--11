using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Domain.Entites
{
    public class Courses
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Level { get; private set; }

        public Courses(int id, string title, string description, string level)
        {
            Id = id;
            Title = title;
            Description = description;
            Level = level;
        }
    }
}
