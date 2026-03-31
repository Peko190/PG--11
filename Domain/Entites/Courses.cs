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
        private readonly List<Lessons> _lessons = new List<Lessons>();

        public Courses(int id, string title, string description, string level)
        {
            Id = id;
            Title = title;
            Description = description;
            Level = level;
        }

        public void AddLesson(Lessons lesson)
        {
            _lessons.Add(lesson);
        }

        public void EditCourses(string title, string description, string level)
        {
            Title = title;
            Description = description;
            Level = level;
        }

        public void ReorderLessons()
        {
            _lessons.Sort((a, b) => a.Order.CompareTo(b.Order));
        }

    }
}
