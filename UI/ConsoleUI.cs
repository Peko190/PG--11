using PG_Тема_11.App;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Console.WriteLine("20:Add new Student");

                Console.Write("Choose: ");
                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1 : Console.WriteLine("");
                        break;
                        
                        
                }
            }


        }

    }
}
