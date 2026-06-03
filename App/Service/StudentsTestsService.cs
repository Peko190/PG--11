using PG_Тема_11.App.Interface;
using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Infrastructure.EFData_Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App.Service
{
    public class StudentsTestsService
    {
        private readonly IStudentsTest testResultRepo;
        private readonly ITestQuestionsRepository questionrepo;
        private readonly ILessonsRepository lessonrepo;
        private readonly ITestsRepository testrepo;


        public StudentsTestsService(IStudentsTest studentsTest,ITestQuestionsRepository testQuestions, ILessonsRepository lessonsrepo, ITestsRepository testsRepository)
        {
            this.testResultRepo = studentsTest;
            this.questionrepo = testQuestions;
            this.lessonrepo = lessonsrepo;
            this.testrepo = testsRepository;
        }
        public IReadOnlyList<TestQuestions> GetQuestionsForTest(int testId)
        {
            var questions = questionrepo.GetAll().Where(q => q.TestId == testId).ToList();
            if (!questions.Any())
            {
                throw new Exception("Този тест все още няма въпроси в базата данни!");
            }
            return questions;
        }
        public void CheckAndScoreDynamicTest(int studentId, int testId, List<string> studentAnswers)
        {
            var test = testrepo.GetById(testId);
            if (test == null)
            {
                throw new Exception("Тестът не е намерен!");
            }

            
            var dbQuestions = questionrepo.GetAll().Where(q => q.TestId == testId).ToList();

            if (studentAnswers.Count != dbQuestions.Count)
            {
                throw new Exception("Невалиден брой подадени отговори!");
            }

            int correctCount = 0;

            
            for (int i = 0; i < dbQuestions.Count; i++)
            {
                if (studentAnswers[i].Trim().Equals(dbQuestions[i].CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    correctCount++;
                }
            }

            
            double finalScore = ((double)correctCount / dbQuestions.Count) * 100;

            
            var testResult = new StudentTestResults(0, studentId, testId, finalScore);
            testResultRepo.Save(testResult);

            Console.WriteLine($"\n--- СИСТЕМНА ПРОВЕРКА ---");
            Console.WriteLine($"Верни отговори: {correctCount} от {dbQuestions.Count}");
            Console.WriteLine($"Резултат: {finalScore:F2}%");
        }


        public double CalculateCourseSuccessRate(int studentId, int courseId)
        {
            var lessonIds = lessonrepo.GetAll()
                .Where(l => l.CourseId == courseId)
                .Select(l => l.Id)
                .ToList();

            var courseTests = testrepo.GetAll()
                .Where(t => t.CourseId == courseId || (t.LessonId.HasValue && lessonIds.Contains(t.LessonId.Value)))
                .Select(t => t.Id)
                .ToList();

            if (!courseTests.Any())
            {
                throw new Exception("Този курс няма намерени тестове за оценка.");
            }

            var studentScores = testResultRepo.GetAll()
                .Where(r => r.StudentId == studentId && courseTests.Contains(r.TestId))
                .Select(r => r.Score)
                .ToList();

            if (!studentScores.Any())
            {
                return 0.0; 
            }

            return studentScores.Average();
        }


    }
}
