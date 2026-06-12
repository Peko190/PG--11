using PG_Тема_11.App;
using PG_Тема_11.App.Service;
using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


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

        // ============================================================
        //  HELPER METHODS FOR FLASHY ASCII UI (added only, no logic changes)
        // ============================================================
        private static void PrintLine(char c = '=', int length = 64, ConsoleColor color = ConsoleColor.DarkCyan)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(new string(c, length));
            Console.ResetColor();
        }

        private static void TypeOut(string text, int delay = 1)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                if (delay > 0) Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        private static void PrintSectionHeader(string title, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.WriteLine();
            Console.ForegroundColor = color;
            Console.WriteLine("  .------------------------------------------------------------.");
            Console.WriteLine($"  |  >>> {title.PadRight(54)}|");
            Console.WriteLine("  '------------------------------------------------------------'");
            Console.ResetColor();
        }

        private static void PrintSuccess(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  [ OK ]  {text}");
            Console.ResetColor();
        }

        private static void PrintError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [ !! ]  {text}");
            Console.ResetColor();
        }

        private static void PrintInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PressEnterToContinue()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            Console.WriteLine("  ........................................................");
            Console.Write("   >> Press ENTER to return to the menu... ");
            Console.ResetColor();
            Console.ReadLine();
        }

        private static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                " ###   ###  ####  #####   #####   ####  ####  #####  #### #   #  ### \n" +
                "#     #   # #   #   #       #     #   # #   # #     #     #  #  #   #\n" +
                "# ### ##### ####    #       #     ####  ####  ###    ###  ###   #   #\n" +
                "#   # #   # #   #   #       #     #     # #   #         # #  #  #   #\n" +
                " ###  #   # ####  #####   #####   #     #  #  ##### ####  #   #  ### "
            );
            Console.ResetColor();
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("*=========================================================*");
                Console.ResetColor();
                PrintBanner();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("*=========================================================*");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("     _ __ ___   ___ _ __  _   _ ");
                Console.WriteLine("    | '_ ` _ \\ / _ \\ '_ \\| | | |");
                Console.WriteLine("    | | | | | |  __/ | | | |_| |");
                Console.WriteLine("    |_| |_| |_|\\___|_| |_|\\__,_|");
                Console.ResetColor();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(" +========================================================+");
                Console.ResetColor();

                PrintMenuRow("1", "Create new Course", ConsoleColor.White);
                PrintMenuRow("2", "Edit a course", ConsoleColor.White);
                PrintMenuRow("3", "Add Lessons", ConsoleColor.White);
                PrintMenuRow("4", "Edit Lessons", ConsoleColor.White);
                PrintMenuRow("5", "Add new Student", ConsoleColor.White);
                PrintMenuRow("6", "Enrol", ConsoleColor.Green);
                PrintMenuRow("7", "Unenrol", ConsoleColor.Red);
                PrintMenuRow("8", "Show student progress", ConsoleColor.Cyan);
                PrintMenuRow("9", "Complete lesson", ConsoleColor.Cyan);
                PrintMenuRow("10", "Create test", ConsoleColor.Yellow);
                PrintMenuRow("11", "Start Test", ConsoleColor.Yellow);
                PrintMenuRow("12", "TakeDynamicTestUI()", ConsoleColor.Yellow);
                PrintMenuRow("13", "ViewCourseSuccessRateUI()", ConsoleColor.Cyan);
                PrintMenuRow("14", "CheckIfCourseIsCompletedUI() [GRAD]", ConsoleColor.Magenta);
                PrintMenuRow("15", "GenerateCertificateUI() [CERT]", ConsoleColor.Magenta);
                PrintMenuRow("16", "Active courses [LIST]", ConsoleColor.Green);
                PrintMenuRow("17", "Report for students in course", ConsoleColor.Gray);
                PrintMenuRow("18", "Report courses with highest success", ConsoleColor.Gray);
                PrintMenuRow("19", "Report most popular courses for period", ConsoleColor.Gray);
                PrintMenuRow("20", "Student training history", ConsoleColor.Gray);

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(" +--------------------------------------------------------+");
                Console.ResetColor();
                PrintMenuRow("0", "Exit", ConsoleColor.Red);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(" +========================================================+");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n   >>> Choose an option: ");
                Console.ResetColor();
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

                if (running)
                {
                    PressEnterToContinue();
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
   ____                 _ _               _ 
  / ___| ___   ___   __| | |__  _   _  ___| |
 | |  _ / _ \ / _ \ / _` | '_ \| | | |/ _ \ |
 | |_| | (_) | (_) | (_| | |_) | |_| |  __/_|
  \____|\___/ \___/ \__,_|_.__/ \__, |\___(_)
                                 |___/        
");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("            Thanks for using the system. See you soon!");
            Console.ResetColor();
        }

        private static void PrintMenuRow(string number, string text, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{number,2}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" : ");
            Console.ForegroundColor = color;
            Console.Write($"{text}");
            int pad = 50 - text.Length - number.Length;
            if (pad < 0) pad = 0;
            Console.Write(new string(' ', pad));
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("|");
            Console.ResetColor();
        }

        public void CreateCourse()
        {
            PrintSectionHeader("CREATE NEW COURSE", ConsoleColor.Green);

            Console.WriteLine("Course Title");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                PrintError("Title cannot be empty!");
                return;
            }

            Console.WriteLine("Enter Description");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                PrintError("Description cannot be empty!");
                return;
            }

            Console.WriteLine("Choose Level");
            Console.WriteLine("0 - Beginner");
            Console.WriteLine("1 - Intermediate");
            Console.WriteLine("2 - Advanced");

            if (!int.TryParse(Console.ReadLine(), out int level) ||
                level < 0 || level > 2)
            {
                PrintError("Invalid level!");
                return;
            }

            var courselevel = (Level)level;

            try
            {
                enrolAndCoursesService.CreateCourse(title, description, courselevel);
                PrintSuccess("Course Created");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }

            Console.ReadLine();
        }

        public void EditCourse()
        {
            PrintSectionHeader("EDIT COURSE", ConsoleColor.Yellow);

            Console.WriteLine("Choose course to edit by id");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                PrintError("Невалидно ID! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Edit Course Title");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                PrintError("Заглавието не може да е празно!");
                return;
            }

            Console.WriteLine("Edit Description");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                PrintError("Описанието не може да е празно!");
                return;
            }

            Console.WriteLine("Edit Level");
            Console.WriteLine("1 - Beginner");
            Console.WriteLine("2 - Intermediate");
            Console.WriteLine("3 - Advanced");

            if (!int.TryParse(Console.ReadLine(), out int levelChoice) || levelChoice < 1 || levelChoice > 3)
            {
                PrintError("Невалиден избор на ниво! Моля въведете 1, 2 или 3.");
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
                PrintSuccess("Course Edited");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        public void AddLesson()
        {
            PrintSectionHeader("ADD LESSON", ConsoleColor.Green);

            Console.WriteLine("Lesson Title");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                PrintError("Заглавието не може да е празно!");
                return;
            }

            Console.WriteLine("Enter Content");
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                PrintError("Съдържанието не може да е празно!");
                return;
            }

            Console.WriteLine("Enter CourseID");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                PrintError("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                lessonsService.AddLessons(title, content, courseId);
                PrintSuccess("Lesson Created");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        public void EditLesson()
        {
            PrintSectionHeader("EDIT LESSON", ConsoleColor.Yellow);

            Console.WriteLine("Choose Lesson to edit by id");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                PrintError("Невалидно ID! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Edit Lesson Title");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                PrintError("Заглавието не може да е празно!");
                return;
            }

            Console.WriteLine("Edit Content");
            string content = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(content))
            {
                PrintError("Съдържанието не може да е празно!");
                return;
            }

            try
            {
                lessonsService.EditLesson(id, title, content);
                PrintSuccess("Lesson Edited");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        public void AddStudent()
        {
            PrintSectionHeader("ADD NEW STUDENT", ConsoleColor.Green);

            Console.WriteLine("Enter FirstName");
            string firstname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstname))
            {
                PrintError("Името не може да е празно!");
                return;
            }

            Console.WriteLine("Enter LastName");
            string lastname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(lastname))
            {
                PrintError("Фамилията не може да е празна!");
                return;
            }

            Console.WriteLine("Enter email");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                PrintError("Невалиден email адрес!");
                return;
            }

            try
            {
                studentsService.AddStudent(firstname, lastname, email);
                PrintSuccess("Student Added");
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        public void EnrolStudentUI()
        {
            PrintSectionHeader("ENROL STUDENT", ConsoleColor.Green);

            Console.WriteLine("Въведете ID на обучаемия:");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на курса:");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                PrintError("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                enrolAndCoursesService.EnrolStudent(studentId, courseId);
                PrintSuccess("Обучаемият беше записан успешно за курса!");
            }
            catch (Exception ex)
            {
                PrintError($"Грешка при записване: {ex.Message}");
            }
        }

        public void UnenrolStudentUI()
        {
            PrintSectionHeader("UNENROL STUDENT", ConsoleColor.Red);

            Console.WriteLine("Въведете ID на обучаемия за отписване:");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на курса:");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                PrintError("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                enrolAndCoursesService.UnenrolStudent(studentId, courseId);
                PrintSuccess("Обучаемият беше отписан успешно от курса!");
            }
            catch (Exception ex)
            {
                PrintError($"Грешка при отписване: {ex.Message}");
            }
        }

        public void ViewProgressUI()
        {
            PrintSectionHeader("STUDENT PROGRESS", ConsoleColor.Cyan);

            Console.WriteLine("Въведете ID на обучаемия:");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на курса:");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                PrintError("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                var enrolment = enrolAndCoursesService.GetStudentProgress(studentId, courseId);
                PrintLine('-', 64, ConsoleColor.Cyan);
                Console.WriteLine($"--- НАПРЕДЪК НА ОБУЧАЕМИЯ ---");
                Console.WriteLine($"Текущ статус: {enrolment.Status}");

                double progress = enrolment.Progress;
                int barWidth = 40;
                int filled = (int)(progress / 100.0 * barWidth);
                if (filled < 0) filled = 0;
                if (filled > barWidth) filled = barWidth;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[");
                Console.Write(new string('#', filled));
                Console.ResetColor();
                Console.Write(new string('-', barWidth - filled));
                Console.WriteLine($"] {progress:F2}%");

                Console.WriteLine($"Завършени уроци (Прогрес): {enrolment.Progress:F2}%");
                PrintLine('-', 64, ConsoleColor.Cyan);
            }
            catch (Exception ex)
            {
                PrintError($"Грешка: {ex.Message}");
            }
        }

        public void CompleteLessonUI()
        {
            PrintSectionHeader("COMPLETE LESSON", ConsoleColor.Cyan);

            Console.WriteLine("Въведете ID на обучаемия:");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на завършения урок:");

            if (!int.TryParse(Console.ReadLine(), out int lessonId) || lessonId <= 0)
            {
                PrintError("Невалидно ID на урок! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                enrolAndCoursesService.MarkLessonAsCompleted(studentId, lessonId);
                PrintSuccess("Урокът беше отбелязан като завършен успешно! Прогресът е обновен.");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    PrintError($"Грешка от базата данни: {ex.InnerException.Message}");
                else
                    PrintError($"Грешка: {ex.Message}");
            }
        }

        public void CreateTestUI()
        {
            PrintSectionHeader("CREATE NEW TEST", ConsoleColor.Yellow);

            Console.WriteLine("=== СЪЗДАВАНЕ НА НОВ ТЕСТ ===");
            Console.Write("Въведете заглавие на теста: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                PrintError("Заглавието не може да е празно!");
                return;
            }

            Console.WriteLine("Към какво е тестът? (1 - Към конкретен урок, 2 - Към целия курс):");

            if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2))
            {
                PrintError("Невалиден избор! Моля въведете 1 или 2.");
                return;
            }

            int? lessonId = null;
            int? courseId = null;

            if (choice == 1)
            {
                Console.Write("Въведете ID на урока: ");
                if (!int.TryParse(Console.ReadLine(), out int lid) || lid <= 0)
                {
                    PrintError("Невалидно ID на урок!");
                    return;
                }
                lessonId = lid;
            }
            else
            {
                Console.Write("Въведете ID на курса: ");
                if (!int.TryParse(Console.ReadLine(), out int cid) || cid <= 0)
                {
                    PrintError("Невалидно ID на курс!");
                    return;
                }
                courseId = cid;
            }

            Console.Write("\nКолко въпроса искате да има този тест?: ");

            if (!int.TryParse(Console.ReadLine(), out int questionsCount) || questionsCount <= 0)
            {
                PrintError("Невалиден брой въпроси! Моля въведете положително цяло число.");
                return;
            }

            List<TestQuestions> questionsList = new List<TestQuestions>();

            for (int i = 0; i < questionsCount; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Console.WriteLine($"  >>>>>>>>>> Question {i + 1} of {questionsCount} <<<<<<<<<<");
                Console.ResetColor();

                Console.Write("Текст на въпроса: ");
                string qText = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(qText))
                {
                    PrintError("Текстът на въпроса не може да е празен!");
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
                PrintSuccess("Тестът беше създаден успешно заедно с въпросите към него.");
            }
            catch (Exception ex)
            {
                PrintError($"Грешка при създаване на теста: {ex.Message}");
            }
        }

        public void StartTestUI()
        {
            PrintSectionHeader("START TEST", ConsoleColor.Yellow);

            Console.WriteLine("Въведете вашето ID на обучаем:");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на теста, който искате да стартирате:");

            if (!int.TryParse(Console.ReadLine(), out int testId) || testId <= 0)
            {
                PrintError("Невалидно ID на тест! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                var test = testsService.StartTest(studentId, testId);
                PrintLine('*', 64, ConsoleColor.Yellow);
                Console.WriteLine($"\n--- ТЕСТЪТ СТАРТИРА УСПЕШНО ---");
                Console.WriteLine($"Заглавие: {test.Title}");
                PrintSuccess("Успех!");
                PrintLine('*', 64, ConsoleColor.Yellow);
            }
            catch (Exception ex)
            {
                PrintError($"Достъпът е отказан: {ex.Message}");
            }
        }

        // 12
        public void TakeDynamicTestUI()
        {
            PrintSectionHeader("DYNAMIC TEST", ConsoleColor.Yellow);

            Console.WriteLine("Въведете ID на обучаемия:");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на теста:");

            if (!int.TryParse(Console.ReadLine(), out int testId) || testId <= 0)
            {
                PrintError("Невалидно ID на тест! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                var questions = studentsTestsService.GetQuestionsForTest(testId);
                List<string> studentAnswers = new List<string>();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\n=== ТЕСТ СТАРТИРА (Общо въпроси: {questions.Count}) ===");
                Console.ResetColor();

                for (int i = 0; i < questions.Count; i++)
                {
                    var q = questions[i];

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine();
                    Console.WriteLine($"  >>>>>>>>>> Question {i + 1} of {questions.Count} <<<<<<<<<<");
                    Console.ResetColor();

                    Console.WriteLine($"{q.QuestionText}");
                    Console.WriteLine($"   A) {q.OptionA}");
                    Console.WriteLine($"   B) {q.OptionB}");
                    Console.WriteLine($"   C) {q.OptionC}");

                    string answer = "";
                    while (answer != "A" && answer != "B" && answer != "C")
                    {
                        Console.Write("Вашият отговор (A/B/C): ");
                        answer = Console.ReadLine().Trim().ToUpper();
                        if (answer != "A" && answer != "B" && answer != "C")
                            PrintError("Невалиден отговор! Въведете A, B или C.");
                    }
                    studentAnswers.Add(answer);
                }

                studentsTestsService.CheckAndScoreDynamicTest(studentId, testId, studentAnswers);
            }
            catch (Exception ex)
            {
                PrintError($"Грешка: {ex.Message}");
            }
        }

        public void ViewCourseSuccessRateUI()
        {
            PrintSectionHeader("COURSE SUCCESS RATE", ConsoleColor.Cyan);

            Console.WriteLine("Въведете ID на обучаемия:");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на обучаем! Моля въведете положително цяло число.");
                return;
            }

            Console.WriteLine("Въведете ID на курса:");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                PrintError("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                double successRate = studentsTestsService.CalculateCourseSuccessRate(studentId, courseId);
                PrintLine('-', 64, ConsoleColor.Cyan);
                Console.WriteLine($"\n--- КРАЕН УСПЕХ ЗА КУРСА ---");
                Console.WriteLine($"Средна успеваемост от изпитите: {successRate:F2}%");

                if (successRate >= 50)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(@"
   *  ***** *   *  ***** *
  ***   *   * * *  *      *
 *****  *   *   *  *****  *
");
                    Console.ResetColor();
                    PrintSuccess("Статус: КУРСЪТ Е ПРЕМИНАТ УСПЕШНО! [GRADUATED]");
                }
                else
                    Console.WriteLine("Статус: Резултатът е под 50%. Курсът не е преминат.");
                PrintLine('-', 64, ConsoleColor.Cyan);
            }
            catch (Exception ex)
            {
                PrintError($"Грешка при изчисление: {ex.Message}");
            }
        }

        // =========================================================================
        // РЕАЛИЗАЦИЯ НА ОПЦИЯ 14: Оценяване и определяне на завършването на курса
        // =========================================================================
        public void CheckIfCourseIsCompletedUI()
        {
            PrintSectionHeader("14. ЗАВЪРШВАНЕ НА КУРС", ConsoleColor.Magenta);

            Console.WriteLine("\n--- 14. ОПРЕДЕЛЯНЕ НА УСПЕШНО ЗАВЪРШВАНЕ НА КУРС ---");
            Console.Write("Въведете ID на студент: ");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на студент! Моля въведете положително цяло число.");
                return;
            }

            Console.Write("Въведете ID на курс: ");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                PrintError("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                bool isCompleted = enrolAndCoursesService.CheckIfCourseIsCompleted(studentId, courseId);

                if (isCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(@"
   *   *  ***  *   *  *
   **  * *   * **  *  *
   * * * *   * * * *  *
   *  ** *   * *  **   
   *   *  ***  *   *  *
");
                    Console.ResetColor();
                    PrintSuccess("\n[СТАТУС]: Курсът е ЗАВЪРШЕН успешно! Статусът е променен на 'Completed' в базата данни. [DONE]");
                }
                else
                {
                    Console.WriteLine("\n[СТАТУС]: Курсът НЕ е завършен.");
                    Console.WriteLine("Условие: Обучаемият трябва да е приключил 100% от лекциите и средната му оценка от теста да е >= 50%.");
                }
            }
            catch (Exception ex)
            {
                PrintError($"Грешка при проверката: {ex.Message}");
            }
        }

        // =========================================================================
        // РЕАЛИЗАЦИЯ НА ОПЦИЯ 15: Издаване на текстов сертификат с уникален номер
        // =========================================================================
        public void GenerateCertificateUI()
        {
            PrintSectionHeader("15. ИЗДАВАНЕ НА СЕРТИФИКАТ", ConsoleColor.Magenta);

            Console.WriteLine("\n--- 15. ИЗДАВАНЕ НА УДОСТОВЕРЕНИЕ (СЕРТИФИКАТ) ---");
            Console.Write("Въведете ID на студент: ");

            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID на студент! Моля въведете положително цяло число.");
                return;
            }

            Console.Write("Въведете ID на курс: ");

            if (!int.TryParse(Console.ReadLine(), out int courseId) || courseId <= 0)
            {
                PrintError("Невалидно ID на курс! Моля въведете положително цяло число.");
                return;
            }

            try
            {
                string certificate = enrolAndCoursesService.GenerateCertificate(studentId, courseId);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Console.WriteLine("   *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                Console.WriteLine("   *                                                      *");
                Console.WriteLine("   *               C E R T I F I C A T E                  *");
                Console.WriteLine("   *                                                      *");
                Console.WriteLine("   *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                Console.ResetColor();
                Console.WriteLine(certificate);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("   *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                PrintError($"\n[ОТКАЗ ЗА ИЗДАВАНЕ]: {ex.Message}");
            }
        }
        // 16
        static void ViewActiveCoursesForStudentUI(EnrolAndCoursesService enrolAndCoursesService)
        {
            PrintSectionHeader("16. АКТИВНИ КУРСОВЕ", ConsoleColor.Green);

            Console.WriteLine("\n=== 16. ПРЕГЛЕД НА АКТИВНИ КУРСОВЕ ЗА ОБУЧАЕМ ===");

            Console.Write("Въведете ID на обучаемия: ");


            if (!int.TryParse(Console.ReadLine(), out int studentId) || studentId <= 0)
            {
                PrintError("Невалидно ID! Моля въведете положително цяло число.");
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
                PrintError($"Грешка: {ex.Message}");
            }
        }




        //17

        public void GenerateStudentsReportUI()
        {
            PrintSectionHeader("17. СПРАВКА ЗА ОБУЧАЕМИ", ConsoleColor.Gray);

            try
            {
                Console.WriteLine("Въведете ID на курс: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int courseId) || courseId <= 0)
                {
                    PrintError("Невалидно ID! Моля въведете положително цяло число.");
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
                PrintError(ex.Message);
            }
        }

        //18



        public void GenerateCourseSuccessReportUI()
        {
            PrintSectionHeader("18. ТОП КУРСОВЕ ПО УСПЕВАЕМОСТ", ConsoleColor.Gray);

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
                    PrintError("Невалиден формат за начална дата! Използвайте dd.MM.yyyy");
                    return;
                }

                if (!DateTime.TryParseExact(endInput, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime endDate))
                {
                    PrintError("Невалиден формат за крайна дата! Използвайте dd.MM.yyyy");
                    return;
                }

                enrolAndCoursesService.GenerateCoursesSuccessReport(startDate, endDate);
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        //19
        public void GenerateMostPopularCoursesReportUI()
        {
            PrintSectionHeader("19. НАЙ-ПОПУЛЯРНИ КУРСОВЕ", ConsoleColor.Gray);

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
                    PrintError("Невалиден формат за начална дата! Използвайте dd.MM.yyyy");
                    return;
                }

                if (!DateTime.TryParseExact(endInput, "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime endDate))
                {
                    PrintError("Невалиден формат за крайна дата! Използвайте dd.MM.yyyy");
                    return;
                }

                enrolAndCoursesService.GenerateMostPopularCoursesReport(startDate, endDate);
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        //20
        public void GenerateStudentHistoryReportUI()
        {
            PrintSectionHeader("20. ИСТОРИЯ НА ОБУЧЕНИЕТО", ConsoleColor.Gray);

            try
            {
                Console.Write("Въведете idto на обучаем: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int studentId) || studentId <= 0)
                {
                    PrintError("Невалидно ID! Моля въведете положително цяло число.");
                    return;
                }

                enrolAndCoursesService.GenerateStudentHistoryReport(studentId);
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }
    }
}