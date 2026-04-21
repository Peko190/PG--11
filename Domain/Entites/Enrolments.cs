using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PG_Тема_11.Domain.Enums.Level;

namespace PG_Тема_11.Domain.Entites
{
    public class Enrolments
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Students Student { get; set; }

        public int CourseId { get; set; }
        public Courses Course { get; set; }
        public EnrollmentStatus Status { get; set; }
        public double Progress { get; set; }
    }
}
