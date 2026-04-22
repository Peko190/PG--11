using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure
{
    public class CoursesAndEnrolmentStorage
    {
        public int NextId { get; set; } = 1;
        public List<Courses>Courses { get; set; } = new List<Courses>();
        public List<Enrolments> Enrolments { get; set; } = new List<Enrolments>();

    }
}
