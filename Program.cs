using Microsoft.EntityFrameworkCore;
using PG_Тема_11.App;
using PG_Тема_11.Infrastructure;
using PG_Тема_11.Infrastructure.EFData_Sql;
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
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=desktop-65gmhv2;Database=PGTEMA;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;
            
            var context = new AppDbContext(options);
            

            context.Database.EnsureCreated();

            ICourseRepository courserepo = new EfCourseRepository(context);
            IEnrollmentRepository enrolrepo = new EfEnrollmentRepository(context);

            var service = new EnrolAndCoursesService(enrolrepo, courserepo);

            var ui = new ConsoleUI(service);

            ui.Run();
        }
    }
}
