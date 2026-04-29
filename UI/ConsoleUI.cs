using PG_Тема_11.App;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.UI
{
    public class ConsoleUI
    {

        private readonly EnrolAndCoursesService enrolAndCoursesService;
        public ConsoleUI(EnrolAndCoursesService enrolAndCoursesService) 
        {
            this.enrolAndCoursesService = enrolAndCoursesService;
        }
        public void Run()
        {
            bool running = true;

            while (running)
            {

                Console.WriteLine("1:Create new Course");
                Console.WriteLine("2:Edit a course");
                Console.WriteLine("3:Add Lessons");
                Console.WriteLine("4:Edit Lessons");
                Console.WriteLine("5:Add new Student");
                Console.WriteLine("6:Add new Student");
                Console.WriteLine("7:Add new Student");
                Console.WriteLine("8:Add new Student");
                Console.WriteLine("9:Add new Student");
                Console.WriteLine("10:Add new Student");
                Console.WriteLine("11:Add new Student");
                Console.WriteLine("12:Add new Student");
                Console.WriteLine("13:Add new Student");
                Console.WriteLine("14:Add new Student");
                Console.WriteLine("15:Add new Student");
                Console.WriteLine("16:Add new Student");
                Console.WriteLine("17:Add new Student");
                Console.WriteLine("18:Add new Student");
                Console.WriteLine("19:Add new Student");
                Console.WriteLine("20:End");

                Console.Write("Choose: ");
                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1 : CreateCourse();
                        break;
                    case 2:
                        EditCourse();
                        break;
                    case 3:
                        AddLesson();
                        break;


                }
            }


        }
        public void CreateCourse()
        {
            Console.WriteLine("Course Title");
            string Title = Console.ReadLine();
            Console.WriteLine("Enter Description");
            string description = Console.ReadLine();
            Console.WriteLine("Choose Level");
            Console.WriteLine("1 - Begginer");
            Console.WriteLine("2 - Intermediate");
            Console.WriteLine("3 - Advanced");
            int level = int.Parse(Console.ReadLine());
            var courselevel = (Level)level;

            try
            {
                enrolAndCoursesService.CreateCourse(Title,description,courselevel);
                Console.WriteLine("Course Created");

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message); 
            }
            Console.ReadLine();

        }
        public void ShowCourses()
        {

        }
        public void EditCourse()
        {
            Console.WriteLine("Choose course to edit by id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(" Edit Course Title");
            string title = Console.ReadLine();
            Console.WriteLine("Edit Description");
            string description = Console.ReadLine();
            Console.WriteLine("Edit Level");
            Console.WriteLine("1 - Begginer");
            Console.WriteLine("2 - Intermediate");
            Console.WriteLine("3 - Advanced");
            int level = int.Parse(Console.ReadLine());
            var courselevel = (Level)level;


            try
            {
                enrolAndCoursesService.EditCourses(id,title, description, courselevel);
                Console.WriteLine("Course Edited");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        public void AddLesson()
        {
            Console.WriteLine();
        }

    }
}
