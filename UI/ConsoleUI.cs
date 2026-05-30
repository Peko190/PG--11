using PG_Тема_11.App;
using PG_Тема_11.App.Service;
using PG_Тема_11.Domain.Entites;
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
        private readonly LessonsService lessonsService;
        private readonly StudentsService studentsService;
        private readonly TestsService testsService;
        public ConsoleUI(EnrolAndCoursesService enrolAndCoursesService, LessonsService lessonsService, StudentsService studentsService, TestsService testsService)
        {
            this.enrolAndCoursesService = enrolAndCoursesService;
            this.lessonsService = lessonsService;
            this.studentsService = studentsService;
            this.testsService = testsService;
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
                Console.WriteLine("6:Enrol");
                Console.WriteLine("7:Unenrol");
                Console.WriteLine("8:Show student progress");
                Console.WriteLine("9:Complete lesson");
                Console.WriteLine("10:Create test");
                Console.WriteLine("11:Start Test");
                Console.WriteLine("12:");
                Console.WriteLine("13:Add new Student");
                Console.WriteLine("14:Add new Student");
                Console.WriteLine("15:");
                Console.WriteLine("16:");
                Console.WriteLine("17:");
                Console.WriteLine("18:");
                Console.WriteLine("19:");
                Console.WriteLine("20:End");

                Console.Write("Choose: ");
                int input = int.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        CreateCourse();
                        break;
                    case 2:
                        EditCourse();
                        break;
                    case 3:
                        AddLesson();
                        break;
                    case 4:
                        EditLesson();
                        break;
                    case 5:
                        AddStudent();
                        break;
                    case 6: EnrolStudentUI();
                        break;
                    case 7:
                        UnenrolStudentUI();
                        break;
                    case 8:
                        ViewProgressUI();
                        break;
                    case 9:
                        CompleteLessonUI();
                        break;
                    case 10:
                           CreateTestUI();
                        break;
                    case 11:
                      StartTestUI();
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
                enrolAndCoursesService.CreateCourse(Title, description, courselevel - 1);
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

            int levelChoice = int.Parse(Console.ReadLine());
            Level courselevel;

            switch (levelChoice)
            {
                case 1:
                    courselevel = Level.Beginner;
                    break;
                case 2:
                    courselevel = Level.Intermediate;
                    break;
                case 3:
                    courselevel = Level.Advanced;
                    break;
                default:
                    courselevel = Level.Beginner;
                    break;
            }

            try
            {
                enrolAndCoursesService.EditCourses(id, title, description, courselevel);
                Console.WriteLine("Course Edited");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddLesson()
        {
            Console.WriteLine("Lesson Title");
            string Title = Console.ReadLine();
            Console.WriteLine("Enter Content ");
            string content = Console.ReadLine();
            Console.WriteLine("Enter courseid ");
            int courseId = int.Parse(Console.ReadLine());


            try
            {
                lessonsService.AddLessons(Title, content, courseId);
                Console.WriteLine("Course Created");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public void EditLesson()
        {
            Console.WriteLine("Choose Lesson to edit by id");  //int id , string title, string content, int order, int courseId)
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(" Edit Lesson Title");
            string title = Console.ReadLine();
            Console.WriteLine("Edit Content");
            string content = Console.ReadLine();

            try
            {
                lessonsService.EditLesson(id, title, content);
                Console.WriteLine("Course Edited");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddStudent()
        {
            Console.WriteLine("Enter FirstName");
            string firstname = Console.ReadLine();

            Console.WriteLine("Enter LastName");
            string lastname = Console.ReadLine();
            Console.WriteLine("Enter email");
            string email = Console.ReadLine();


            try
            {
                studentsService.AddStudent(firstname, lastname, email);
                Console.WriteLine("Student Added");

            }
            catch (Exception ex)
            {

            }
        }
        public void EnrolStudentUI()
        {
            Console.WriteLine("Въведете ID на обучаемия:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете ID на курса:");
            int courseId = int.Parse(Console.ReadLine());

            try
            {
                enrolAndCoursesService.EnrolStudent(studentId, courseId);
                Console.WriteLine("Обучаемият беше записан успешно за курса!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при записване: {ex.Message}");
            }
        }
        public void UnenrolStudentUI()
        {
            Console.WriteLine("Въведете ID на обучаемия за отписване:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете ID на курса:");
            int courseId = int.Parse(Console.ReadLine());

            try
            {
                enrolAndCoursesService.UnenrolStudent(studentId, courseId);
                Console.WriteLine("Обучаемият беше отписан успешно от курса!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при отписване: {ex.Message}");
            }
        }
        public void ViewProgressUI()
        {
            Console.WriteLine("Въведете ID на обучаемия:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете ID на курса:");
            int courseId = int.Parse(Console.ReadLine());

            try
            {
                var enrolment = enrolAndCoursesService.GetStudentProgress(studentId, courseId);
                Console.WriteLine($"--- НАПРЕДЪК НА ОБУЧАЕМИЯ ---");
                Console.WriteLine($"Текущ статус: {enrolment.Status}");
                Console.WriteLine($"Завършени уроци (Прогрес): {enrolment.Progress:F2}%");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
        }

        
        public void CompleteLessonUI()
        {
            Console.WriteLine("Въведете ID на обучаемия:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете ID на курса:");
            int courseId = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете ID на завършения урок:");
            int lessonId = int.Parse(Console.ReadLine());

            try
            {
                enrolAndCoursesService.CompleteLesson(studentId, courseId, lessonId);
                Console.WriteLine("Урокът беше отбелязан като завършен успешно! Прогресът е обновен.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Грешка от базата данни: {ex.InnerException.Message}");
                }
                else
                {
                    Console.WriteLine($"Грешка: {ex.Message}");
                }
            }

        }
        public void CreateTestUI()
        {
            Console.WriteLine("Въведете заглавие на теста:");
            string title = Console.ReadLine();

            Console.WriteLine("Към какво е тестът? (1 - Към конкретен урок, 2 - Към целия курс):");
            int choice = int.Parse(Console.ReadLine());

            int? lessonId = null;
            int? courseId = null;

            if (choice == 1)
            {
                Console.WriteLine("Въведете ID на урока:");
                lessonId = int.Parse(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Въведете ID на курса:");
                courseId = int.Parse(Console.ReadLine());
            }

            try
            {
                testsService.CreateTest(title, lessonId, courseId);
                Console.WriteLine("Тестът беше създаден успешно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
        }

        
        public void StartTestUI()
        {
            Console.WriteLine("Въведете вашето ID на обучаем:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете ID на теста, който искате да стартирате:");
            int testId = int.Parse(Console.ReadLine());

            try
            {
                var test = testsService.StartTest(studentId, testId);
                Console.WriteLine($"\n--- ТЕСТЪТ СТАРТИРА УСПЕШНО ---");
                Console.WriteLine($"Заглавие: {test.Title}");
                Console.WriteLine("Успех! (Тук можете да добавите логика за отговаряне на въпроси)...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Достъпът е отказан: {ex.Message}");
            }
        }
    }
}
