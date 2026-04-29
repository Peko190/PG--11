using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Domain.Entites
{
    public class Lessons
    {
        public int Id { get; private set; }
        public string Title { get;  set; }
        public string Content { get;  set; }
        public int Order { get;  set; }
        public int CourseId { get;  set; }

        public Lessons(int id , string title, string content, int order, int courseId)
        {
            Title = title;
            Content = content;
            Order = order;
            CourseId = courseId;
        }

        

       

        
    }
}
