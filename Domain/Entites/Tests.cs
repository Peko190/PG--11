using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Domain.Entites
{
    public class Tests
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int CourseId { get; set; }
        public int LessonId { get; set; }
    }
}
