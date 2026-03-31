using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Domain.Entites
{
    internal class Progress
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int LessonId { get; set; }

        public bool isCompleted { get; set; }
    }

}
