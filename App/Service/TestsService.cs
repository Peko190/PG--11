using PG_Тема_11.App.Interface;
using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App.Service
{
    public class TestsService
    {
        private readonly ITestsRepository testrepo;
        private readonly IEnrollmentRepository enrolrepo;
        private readonly ILessonsRepository lessonrepo;
        private readonly ITestQuestionsRepository questionrepo;
        public TestsService(IEnrollmentRepository enrolrepo, ITestsRepository testsRepository, ILessonsRepository lessonsrepo, ITestQuestionsRepository questionrepo    )
        {
            this.testrepo = testsRepository;
            this.enrolrepo = enrolrepo;
            this.lessonrepo = lessonsrepo;
            this.questionrepo = questionrepo;
        }
        public void CreateTestWithQuestions(string title, int? lessonId, int? courseId, List<TestQuestions> questions)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Заглавието на теста не може да бъде празно!");
            }

            
            var newTest = new Tests(0, title, lessonId, courseId);
            testrepo.Save(newTest); 

            if (questions == null || !questions.Any())
            {
                throw new Exception("Тестът трябва да има поне един въпрос!");
            }

            
            foreach (var question in questions)
            {
                question.TestId = newTest.Id; 
                questionrepo.Save(question);
            }

            Console.WriteLine($"\n[Система]: Успешно създаден тест '{title}' с {questions.Count} въпроса!");
        }


        public Tests StartTest(int studentId, int testId)
        {
            
            var test = testrepo.GetById(testId);
            if (test == null)
            {
                throw new Exception("Тестът не е намерен!");
            }

            
            int targetedCourseId = test.CourseId ?? 0;

            if (targetedCourseId == 0 && test.LessonId.HasValue)
            {
                var lesson = lessonrepo.GetById(test.LessonId.Value);
                if (lesson != null) targetedCourseId = lesson.CourseId;
            }

            var enrolment = enrolrepo.GetAll()
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == targetedCourseId);

            if (enrolment == null || enrolment.Status == EnrollmentStatus.Cancelled)
            {
                throw new Exception("Нямате достъп до този тест! Трябва да сте активно записани в курса.");
            }

            return test;
        }
    }
}
