using Microsoft.EntityFrameworkCore;
using PG_Тема_11.App;
using PG_Тема_11.App.Interface;
using PG_Тема_11.App.Service;
using PG_Тема_11.Infrastructure;
using PG_Тема_11.Infrastructure.EFData_Sql;
using PG_Тема_11.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=desktop-65gmhv2;Database=PGTEMA;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;
            
            var context = new AppDbContext(options);



             

        context.Database.EnsureCreated();

            ICourseRepository courserepo = new EfCourseRepository(context);
            IEnrollmentRepository enrolrepo = new EfEnrollmentRepository(context);
            ILessonsRepository lessonsrepo = new EfLessonsRepository(context);
            IStudentsRepository studentrepo = new EfStudentsRepository(context);
            ITestsRepository testsRepository = new EfTestsRepository(context);
            IStudentsTest studentstests = new EfStudentTestResultsRepository(context);
            ITestQuestionsRepository questionrepo = new EfTestQuestionRepository(context);
            IProgressRepository progressrepo = new EfProgressRepository(context);


            var service5 = new StudentsTestsService(studentstests, questionrepo, lessonsrepo, testsRepository);
            var service1 = new EnrolAndCoursesService(enrolrepo, courserepo, lessonsrepo, service5, progressrepo);
            var service2 = new LessonsService(lessonsrepo);
            var service3 = new StudentsService(studentrepo);
            var service4 = new TestsService(enrolrepo,testsRepository,lessonsrepo,questionrepo);
           

            var ui = new ConsoleUI(service1,service2,service3,service4,service5);

            ui.Run();
        }
    }
}
