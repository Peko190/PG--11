using PG_Тема_11.App;
using PG_Тема_11.Infrastructure;
using PG_Тема_11.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storage = new Storage  ("storage.json");

            ICourseRepository courserepo = new CourseRepository(storage);
            IEnrollmentRepository enrolrepo= new EnrollmentRepository(storage);


            var service = new EnrolAndCoursesService (enrolrepo,courserepo);

            var ui = new ConsoleUI(service);
        }
    }
}
