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
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty!");
                return;
            }

            Console.WriteLine("Enter Description");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description cannot be empty!");
                return;
            }

            Console.WriteLine("Choose Level");
            Console.WriteLine("0 - Beginner");
            Console.WriteLine("1 - Intermediate");
            Console.WriteLine("2 - Advanced");

            if (!int.TryParse(Console.ReadLine(), out int level) ||
                level < 0 || level > 2)
            {
                Console.WriteLine("Invalid level!");
                return;
            }

            var courselevel = (Level)level;

            try
            {
                enrolAndCoursesService.CreateCourse(title, description, courselevel);
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

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Невалидно ID! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Edit Course Title");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Заглавието не може да е празно!");
                return;
            }

            Console.WriteLine("Edit Description");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Описанието не може да е празно!");
                return;
            }

            Console.WriteLine("Edit Level");
            Console.WriteLine("1 - Beginner");
            Console.WriteLine("2 - Intermediate");
            Console.WriteLine("3 - Advanced");

            if (!int.TryParse(Console.ReadLine(), out int levelChoice) || levelChoice < 1 || levelChoice > 3)
            {
                Console.WriteLine("Невалиден избор на ниво! Моля въведете 1, 2 или 3.");
                return;
            }

            Level courselevel;
            switch (levelChoice)
            {
                case 1: courselevel = Level.Beginner; break;
                case 2: courselevel = Level.Intermediate; break;
                case 3: courselevel = Level.Advanced; break;
                default: courselevel = Level.Beginner; break;
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
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Заглавието не може да е празно!");
                return;
            }

            Console.WriteLine("Enter Content");
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Съдържанието не може да е празно!");
                return;
            }

            Console.WriteLine("Enter CourseID");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                Console.WriteLine("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                lessonsService.AddLessons(title, content, courseId);
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

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Невалидно ID! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Edit Lesson Title");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Заглавието не може да е празно!");
                return;
            }

            Console.WriteLine("Edit Content");
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Съдържанието не може да е празно!");
                return;
            }

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

            if (string.IsNullOrWhiteSpace(firstname))
            {
                Console.WriteLine("Името не може да е празно!");
                return;
            }

            Console.WriteLine("Enter LastName");
            string lastname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(lastname))
            {
                Console.WriteLine("Фамилията не може да е празна!");
                return;
            }

            Console.WriteLine("Enter email");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("Невалиден email адрес!");
                return;
            }

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

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на курса:");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                Console.WriteLine("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

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

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на курса:");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                Console.WriteLine("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

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

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на курса:");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                Console.WriteLine("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

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

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на завършения урок:");

            if (!int.TryParse(Console.ReadLine(), out int lessonId) || lessonId <= 0)
            {
                Console.WriteLine("Невалидно ID на урок! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                enrolAndCoursesService.MarkLessonAsCompleted(studentId, lessonId);
                Console.WriteLine("Урокът беше отбелязан като завършен успешно! Прогресът е обновен.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Console.WriteLine($"Грешка от базата данни: {ex.InnerException.Message}");
                else
                    Console.WriteLine($"Грешка: {ex.Message}");
            }
        }

        public void CreateTestUI()
        {
            Console.WriteLine("=== СЪЗДАВАНЕ НА НОВ ТЕСТ ===");
            Console.Write("Въведете заглавие на теста: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Заглавието не може да е празно!");
                return;
            }

            Console.WriteLine("Към какво е тестът? (1 - Към конкретен урок, 2 - Към целия курс):");

            if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Невалиден избор! Моля въведете 1 или 2.");
                return;
            }

            int? lessonId = null;
            int? courseId = null;

            if (choice == 1)
            {
                Console.Write("Въведете ID на урока: ");
                if (!int.TryParse(Console.ReadLine(), out int lid) || lid <= 0)
                {
                    Console.WriteLine("Невалидно ID на урок!");
                    return;
                }
                lessonId = lid;
            }
            else
            {
                Console.Write("Въведете ID на курса: ");
                if (!int.TryParse(Console.ReadLine(), out int cid) || cid <= 0)
                {
                    Console.WriteLine("Невалидно ID на курс!");
                    return;
                }
                courseId = cid;
            }

            Console.Write("\nКолко въпроса искате да има този тест?: ");

            if (!int.TryParse(Console.ReadLine(), out int questionsCount) || questionsCount <= 0)
            {
                Console.WriteLine("Невалиден брой въпроси! Моля въведете положително цяло число.");
                return;
            }

            List<TestQuestions> questionsList = new List<TestQuestions>();

            for (int i = 0; i < questionsCount; i++)
            {
                Console.WriteLine($"\n--- Въвеждане на въпрос {i + 1} ---");

                Console.Write("Текст на въпроса: ");
                string qText = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(qText))
                {
                    Console.WriteLine("Текстът на въпроса не може да е празен!");
                    return;
                }

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

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на теста, който искате да стартирате:");

            if (!int.TryParse(Console.ReadLine(), out int testId) || testId <= 0)
            {
                Console.WriteLine("Невалидно ID на тест! Моля въведете положително цяло число.");
                return;
            }

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

        // 12
        public void TakeDynamicTestUI()
        {
            Console.WriteLine("Въведете ID на обучаемия:");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на теста:");

            if (!int.TryParse(Console.ReadLine(), out int testId) || testId <= 0)
            {
                Console.WriteLine("Невалидно ID на тест! Моля въведете положително цяло число.");
                return;
            }

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

                    string answer = "";
                    while (answer != "A" && answer != "B" && answer != "C")
                    {
                        Console.Write("Вашият отговор (A/B/C): ");
                        answer = Console.ReadLine().Trim().ToUpper();
                        if (answer != "A" && answer != "B" && answer != "C")
                            Console.WriteLine("Невалиден отговор! Въведете A, B или C.");
                    }
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

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на курса:");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                Console.WriteLine("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                double successRate = studentsTestsService.CalculateCourseSuccessRate(studentId, courseId);
                Console.WriteLine($"\n--- КРАЕН УСПЕХ ЗА КУРСА ---");
                Console.WriteLine($"Средна успеваемост от изпитите: {successRate:F2}%");

                if (successRate >= 50)
                    Console.WriteLine("Статус: КУРСЪТ Е ПРЕМИНАТ УСПЕШНО! 🎓");
                else
                    Console.WriteLine("Статус: Резултатът е под 50%. Курсът не е преминат.");
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

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на студент! Моля въведете положително цяло число.");
                return;
            }

            Console.Write("Въведете ID на курс: ");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                Console.WriteLine("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                bool isCompleted = enrolAndCoursesService.CheckIfCourseIsCompleted(studentId, courseId);

                if (isCompleted)
                    Console.WriteLine("\n[СТАТУС]: Курсът е ЗАВЪРШЕН успешно! Статусът е променен на 'Completed' в базата данни. 🎉");
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

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID на студент! Моля въведете положително цяло число.");
                return;
            }

            Console.Write("Въведете ID на курс: ");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                Console.WriteLine("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

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
        // 16
        static void ViewActiveCoursesForStudentUI(EnrolAndCoursesService enrolAndCoursesService)
        {
            Console.WriteLine("\n=== 16. ПРЕГЛЕД НА АКТИВНИ КУРСОВЕ ЗА ОБУЧАЕМ ===");

            Console.Write("Въведете ID на обучаемия: ");


            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                Console.WriteLine("Невалидно ID! Моля въведете положително цяло число.");
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
                string startInput = Console.ReadLine();

                Console.Write("Крайна дата (dd.MM.yyyy): ");
                string endInput = Console.ReadLine();

                if (!DateTime.TryParseExact(startInput, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime startDate))
                {
                    Console.WriteLine("Невалиден формат за начална дата! Използвайте dd.MM.yyyy");
                    return;
                }

                if (!DateTime.TryParseExact(endInput, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime endDate))
                {
                    Console.WriteLine("Невалиден формат за крайна дата! Използвайте dd.MM.yyyy");
                    return;
                }

                enrolAndCoursesService.GenerateCoursesSuccessReport(startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //19
        public void GenerateMostPopularCoursesReportUI()
        {
            try
            {
                Console.Write("Начална дата (dd.MM.yyyy): ");
                string startInput = Console.ReadLine();

                Console.Write("Крайна дата (dd.MM.yyyy): ");
                string endInput = Console.ReadLine();

                if (!DateTime.TryParseExact(startInput, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime startDate))
                {
                    Console.WriteLine("Невалиден формат за начална дата! Използвайте dd.MM.yyyy");
                    return;
                }

                if (!DateTime.TryParseExact(endInput, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime endDate))
                {
                    Console.WriteLine("Невалиден формат за крайна дата! Използвайте dd.MM.yyyy");
                    return;
                }

                enrolAndCoursesService.GenerateMostPopularCoursesReport(startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //20
        public void GenerateStudentHistoryReportUI()
        {
            try
            {
                Console.Write("Въведете idto на обучаем: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int studentId) || studentId <= 0)
                {
                    Console.WriteLine("Невалидно ID! Моля въведете положително цяло число.");
                    return;
                }

                enrolAndCoursesService.GenerateStudentHistoryReport(studentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}