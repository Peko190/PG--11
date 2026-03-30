using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Domain.Entites
{
    public class Lessons
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int Order { get; private set; }
        public int CourseId { get; private set; }

        public Lessons(string title, string content, int order, int courseId)
        {
            Title = title;
            Content = content;
            Order = order;
            CourseId = courseId;
        }


    }
}
