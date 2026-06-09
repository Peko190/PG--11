using PG_Тема_11.App;
using PG_Тема_11.App.Service;
using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PG_Тема_11.UI
{
    public class ConsoleUI
    {
        private readonly EnrolAndCoursesService enrolAndCoursesService;
        private readonly LessonsService lessonsService;
        private readonly StudentsService studentsService;
        private readonly TestsService testsService;
        private readonly StudentsTestsService studentsTestsService;

        public ConsoleUI(
            EnrolAndCoursesService enrolAndCoursesService,
            LessonsService lessonsService,
            StudentsService studentsService,
            TestsService testsService,
            StudentsTestsService studentsTestsService)
        {
            this.enrolAndCoursesService = enrolAndCoursesService;
            this.lessonsService = lessonsService;
            this.studentsService = studentsService;
            this.testsService = testsService;
            this.studentsTestsService = studentsTestsService;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== МЕНЮ СИСТЕМА ===");
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
                Console.WriteLine("12:TakeDynamicTestUI()");
                Console.WriteLine("13:ViewCourseSuccessRateUI()");
                Console.WriteLine("14:CheckIfCourseIsCompletedUI() 🎓");
                Console.WriteLine("15:GenerateCertificateUI() 📜");
                Console.WriteLine("16:Active courses📜");
                Console.WriteLine("17:Report for students in course");
                Console.WriteLine("18:Report courses with highest success rate");
                Console.WriteLine("19:Report most popular courses for period");
                Console.WriteLine("20:Student training history");
                Console.WriteLine("0:Exit");

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
                    case 6:
                        EnrolStudentUI();
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
                    case 12:
                        TakeDynamicTestUI();
                        break;
                    case 13:
                        ViewCourseSuccessRateUI();
                        break;
                    case 14:
                        CheckIfCourseIsCompletedUI(); 
                        break;
                    case 15:
                        GenerateCertificateUI();     
                        break;
                    case 16:
                        ViewActiveCoursesForStudentUI(enrolAndCoursesService);
                        break;
                    case 17:
                        GenerateStudentsReportUI();
                        break;
                    case 18:
                        GenerateCourseSuccessReportUI();
                        break;
                    case 19:
                        GenerateMostPopularCoursesReportUI();
                        break;
                    case 20:
                        GenerateStudentHistoryReportUI();
                        break;
                    case 0:
                        running = false;
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
            Console.WriteLine("1 - Beginner");
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

        public void EditCourse()
        {
            Console.WriteLine("Choose course to edit by id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(" Edit Course Title");
            string title = Console.ReadLine();
            Console.WriteLine("Edit Description");
            string description = Console.ReadLine();
            Console.WriteLine("Edit Level");

            Console.WriteLine("1 - Beginner");
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
                Console.WriteLine("Lesson Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditLesson()
        {
            Console.WriteLine("Choose Lesson to edit by id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(" Edit Lesson Title");
            string title = Console.ReadLine();
            Console.WriteLine("Edit Content");
            string content = Console.ReadLine();

            try
            {
                lessonsService.EditLesson(id, title, content);
                Console.WriteLine("Lesson Edited");
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
                Console.WriteLine(ex.Message);
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

            Console.WriteLine("Въведете ID на завършения урок:");
            int lessonId = int.Parse(Console.ReadLine());

            try
            {

                enrolAndCoursesService.MarkLessonAsCompleted(studentId, lessonId);
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
            Console.WriteLine("=== СЪЗДАВАНЕ НА НОВ ТЕСТ ===");
            Console.Write("Въведете заглавие на теста: ");
            string title = Console.ReadLine();

            Console.WriteLine("Към какво е тестът? (1 - Към конкретен урок, 2 - Към целия курс):");
            int choice = int.Parse(Console.ReadLine());

            int? lessonId = null;
            int? courseId = null;

            if (choice == 1)
            {
                Console.Write("Въведете ID на урока: ");
                lessonId = int.Parse(Console.ReadLine());
            }
            else
            {
                Console.Write("Въведете ID на курса: ");
                courseId = int.Parse(Console.ReadLine());
            }

            Console.Write("\nКолко въпроса искате да има този тест?: ");
            int questionsCount = int.Parse(Console.ReadLine());

            List<TestQuestions> questionsList = new List<TestQuestions>();

            for (int i = 0; i < questionsCount; i++)
            {
                Console.WriteLine($"\n--- Въвеждане на въпрос {i + 1} ---");

                Console.Write("Текст на въпроса: ");
                string qText = Console.ReadLine();

                Console.Write("Опция A: ");
                string optA = Console.ReadLine();

                Console.Write("Опция B: ");
                string optB = Console.ReadLine();

                Console.Write("Опция C: ");
                string optC = Console.ReadLine();

                string correctOpt = "";
                while (correctOpt != "A" && correctOpt != "B" && correctOpt != "C")
                {
                    Console.Write("Кой е верният отговор (Въведете само главна буква A, B или C): ");
                    correctOpt = Console.ReadLine().Trim().ToUpper();
                }

                var question = new TestQuestions(0, 0, qText, optA, optB, optC, correctOpt);
                questionsList.Add(question);
            }

            try
            {
                testsService.CreateTestWithQuestions(title, lessonId, courseId, questionsList);
                Console.WriteLine("Тестът беше създаден успешно заедно с въпросите към него.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при създаване на теста: {ex.Message}");
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
                Console.WriteLine("Успех!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Достъпът е отказан: {ex.Message}");
            }
        }

        public void TakeDynamicTestUI()
        {
            Console.WriteLine("Въведете ID на обучаемия:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете ID на теста:");
            int testId = int.Parse(Console.ReadLine());

            try
            {
                var questions = studentsTestsService.GetQuestionsForTest(testId);
                List<string> studentAnswers = new List<string>();

                Console.WriteLine($"\n=== ТЕСТ СТАРТИРА (Общо въпроси: {questions.Count}) ===");

                for (int i = 0; i < questions.Count; i++)
                {
                    var q = questions[i];
                    Console.WriteLine($"\nВъпрос {i + 1}: {q.QuestionText}");
                    Console.WriteLine($"A) {q.OptionA}");
                    Console.WriteLine($"B) {q.OptionB}");
                    Console.WriteLine($"C) {q.OptionC}");

                    Console.Write("Вашият отговор (A/B/C): ");
                    string answer = Console.ReadLine().Trim().ToUpper();
                    studentAnswers.Add(answer);
                }

                studentsTestsService.CheckAndScoreDynamicTest(studentId, testId, studentAnswers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
        }

        public void ViewCourseSuccessRateUI()
        {
            Console.WriteLine("Въведете ID на обучаемия:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете ID на курса:");
            int courseId = int.Parse(Console.ReadLine());

            try
            {
                double successRate = studentsTestsService.CalculateCourseSuccessRate(studentId, courseId);
                Console.WriteLine($"\n--- КРАЕН УСПЕХ ЗА КУРСА ---");
                Console.WriteLine($"Средна успеваемост от изпитите: {successRate:F2}%");

                if (successRate >= 50)
                {
                    Console.WriteLine("Статус: КУРСЪТ Е ПРЕМИНАТ УСПЕШНО! 🎓");
                }
                else
                {
                    Console.WriteLine("Статус: Резултатът е под 50%. Курсът не е преминат.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при изчисление: {ex.Message}");
            }
        }

        // =========================================================================
        // РЕАЛИЗАЦИЯ НА ОПЦИЯ 14: Оценяване и определяне на завършването на курса
        // =========================================================================
        public void CheckIfCourseIsCompletedUI()
        {
            Console.WriteLine("\n--- 14. ОПРЕДЕЛЯНЕ НА УСПЕШНО ЗАВЪРШВАНЕ НА КУРС ---");
            Console.Write("Въведете ID на студент: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Въведете ID на курс: ");
            int courseId = int.Parse(Console.ReadLine());

            try
            {
                bool isCompleted = enrolAndCoursesService.CheckIfCourseIsCompleted(studentId, courseId);

                if (isCompleted)
                {
                    Console.WriteLine("\n[СТАТУС]: Курсът е ЗАВЪРШЕН успешно! Статусът е променен на 'Completed' в базата данни. 🎉");
                }
                else
                {
                    Console.WriteLine("\n[СТАТУС]: Курсът НЕ е завършен.");
                    Console.WriteLine("Условие: Обучаемият трябва да е приключил 100% от лекциите и средната му оценка от теста да е >= 50%.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при проверката: {ex.Message}");
            }
        }

        // =========================================================================
        // РЕАЛИЗАЦИЯ НА ОПЦИЯ 15: Издаване на текстов сертификат с уникален номер
        // =========================================================================
        public void GenerateCertificateUI()
        {
            Console.WriteLine("\n--- 15. ИЗДАВАНЕ НА УДОСТОВЕРЕНИЕ (СЕРТИФИКАТ) ---");
            Console.Write("Въведете ID на студент: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Въведете ID на курс: ");
            int courseId = int.Parse(Console.ReadLine());

            try
            {
                string certificate = enrolAndCoursesService.GenerateCertificate(studentId, courseId);
                Console.WriteLine(certificate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ОТКАЗ ЗА ИЗДАВАНЕ]: {ex.Message}");
            }
        }
        static void ViewActiveCoursesForStudentUI(EnrolAndCoursesService enrolAndCoursesService)
        {
            Console.WriteLine("\n=== 16. ПРЕГЛЕД НА АКТИВНИ КУРСОВЕ ЗА ОБУЧАЕМ ===");
            Console.Write("Въведете ID на обучаемия: ");

            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Невалидно ID!");
                return;
            }

            try
            {
                var activeCourses = enrolAndCoursesService.GetActiveCoursesForStudent(studentId);

                if (activeCourses == null || activeCourses.Count == 0)
                {
                    Console.WriteLine("Този обучаем няма активни записвания в нито един курс в момента.");
                    return;
                }

                Console.WriteLine($"\nАктивни курсове за обучаем с ID {studentId}:");
                Console.WriteLine(new string('-', 50));
                foreach (var course in activeCourses)
                {
                    Console.WriteLine($"ID: {course.Id} | Заглавие: {course.Title} | Ниво: {course.level}");
                }
                Console.WriteLine(new string('-', 50));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }

            

            
        }

        //17

        public void GenerateStudentsReportUI()
        {
            try
            {
                Console.WriteLine("Въведете ID на курс: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int courseId) || courseId <= 0)
                {
                    Console.WriteLine("Невалидно ID! Моля въведете положително цяло число.");
                    return;
                }

                var enrolments = enrolAndCoursesService.GetStudentsInCourse(courseId);

                Console.WriteLine("\n ===== СПРАВКА ЗА ОБУЧАЕМИ =====");

                foreach (var enrolment in enrolments)
                {
                    Console.WriteLine(
                    $"ID: {enrolment.Student.Id} | " +
                    $"{enrolment.Student.FirstName} {enrolment.Student.LastName} | " +
                    $"Email: {enrolment.Student.Email} | " +
                    $"Статус: {enrolment.Status} | " +
                    $"Прогрес: {enrolment.Progress:F2}%");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //18

        public void GenerateCourseSuccessReportUI()
        {
            try
            {
                Console.Write("Начална дата (dd.MM.yyyy): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Крайна дата (dd.MM.yyyy): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                enrolAndCoursesService.GenerateCoursesSuccessReport(
                    startDate,
                    endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GenerateStudentHistoryReportUI()
        {
            try
            {
                Console.Write("Въведете idto на обучаем: ");
                int studentId = int.Parse(Console.ReadLine());

                enrolAndCoursesService.GenerateStudentHistoryReport(studentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}