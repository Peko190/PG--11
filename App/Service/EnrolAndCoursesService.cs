using PG_Тема_11.App.Interface;
using PG_Тема_11.App.Service;
using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PG_Тема_11.App
{
    public class EnrolAndCoursesService
    {
        private readonly ICourseRepository courserepo;
        private readonly IEnrollmentRepository enrolrepo;
        private readonly ILessonsRepository lessonrepo;
        private readonly IProgressRepository progressRepo;
        private readonly StudentsTestsService studentsTestsService;

        public EnrolAndCoursesService(
 IEnrollmentRepository enrolrepo,
 ICourseRepository courserepo,
 ILessonsRepository lessonrepo,
 StudentsTestsService studentsTestsService, 
 IProgressRepository progressRepo)
        {
            this.enrolrepo = enrolrepo;
            this.courserepo = courserepo;
            this.lessonrepo = lessonrepo;
            this.studentsTestsService = studentsTestsService;
            this.progressRepo = progressRepo;
        }

        public void CreateCourse(string title, string description, Level level)
        {
            var course = new Courses(0, title, description, level);
            courserepo.Save(course);
        }

        public void EditCourses(int id, string title, string description, Level level)
        {
            var course = courserepo.GetById(id);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            course.Title = title;
            course.Description = description;
            course.level = level;
            courserepo.Save(course);
        }

        public IReadOnlyList<Courses> GetAll()
        {
            return courserepo.GetAll();
        }

        //----------------------------------------------------------------

        public void EnrolStudent(int studentId, int courseId)
        {
            var course = courserepo.GetById(courseId);
            if (course == null)
            {
                throw new Exception("Курсът не е намерен!");
            }

            bool alreadyEnrolled = enrolrepo.GetAll()
                .Any(e => e.StudentId == studentId && e.CourseId == courseId);

            if (alreadyEnrolled)
            {
                throw new Exception("Студентът вече е записан за този курс!");
            }

            var newEnrolment = new Enrolments(0, studentId, courseId, EnrollmentStatus.Active, 0.0, DateTime.Now);
            enrolrepo.Save(newEnrolment);
        }

        public void UnenrolStudent(int studentId, int courseId)
        {
            var enrolment = enrolrepo.GetAll()
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrolment == null)
            {
                throw new Exception("Обучаемият не е записан в този курс!");
            }

            if (DateTime.Now > enrolment.EnrolmentDate.AddDays(3))
            {
                throw new Exception("Срокът за отписване (3 дни от датата на записване) е изтекъл!");
            }

            enrolment.Status = EnrollmentStatus.Cancelled;
            enrolrepo.Save(enrolment);
        }
        public Enrolments GetStudentProgress(int studentId, int courseId)
        {
            var enrolment = enrolrepo.GetAll().FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrolment == null)
            {
                throw new Exception("Обучаемият не е записан в този курс!");
            }
            return enrolment;
        }
        public void MarkLessonAsCompleted(int studentId, int lessonId)
        {
            var lesson = lessonrepo.GetById(lessonId);
            if (lesson == null) throw new Exception("Урокът не е намерен!");

            var alreadyCompleted = progressRepo.GetAll()
                .Any(p => p.StudentId == studentId && p.LessonId == lessonId);

            if (alreadyCompleted)
            {
                throw new Exception("Този урок вече е отбелязан като завършен!");
            }

            var progressEntry = new Progress(0, studentId, lessonId);
            progressRepo.Save(progressEntry);

            UpdateEnrolmentProgressPercentage(studentId, lesson.CourseId);
            Console.WriteLine($"\n[Система]: Урокът '{lesson.Title}' беше отбелязан като завършен успешно!");
        }

        private void UpdateEnrolmentProgressPercentage(int studentId, int courseId)
        {
            var enrolment = enrolrepo.GetAll().FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrolment == null) return;

            var allLessons = lessonrepo.GetAll().Where(l => l.CourseId == courseId).Select(l => l.Id).ToList();
            if (!allLessons.Any()) return;

            var completedCount = progressRepo.GetAll().Count(p => p.StudentId == studentId && allLessons.Contains(p.LessonId));

            enrolment.Progress = ((double)completedCount / allLessons.Count) * 100;
            enrolrepo.Save(enrolment);
        }

        
        public bool CheckIfCourseIsCompleted(int studentId, int courseId)
        {
            var enrolment = enrolrepo.GetAll().FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrolment == null) throw new Exception("Обучаемият не е записан в този курс!");

            var allCourseLessons = lessonrepo.GetAll().Where(l => l.CourseId == courseId).Select(l => l.Id).ToList();
            if (!allCourseLessons.Any()) throw new Exception("Курсът няма лекции.");

            var completedLessonsCount = progressRepo.GetAll().Count(p => p.StudentId == studentId && allCourseLessons.Contains(p.LessonId));

            bool allLessonsDone = completedLessonsCount == allCourseLessons.Count;
            double successRate = studentsTestsService.CalculateCourseSuccessRate(studentId, courseId);
            bool examPassed = successRate >= 50.0;

            if (allLessonsDone && examPassed)
            {
                enrolment.Status = EnrollmentStatus.Completed;
                enrolment.Progress = 100.0;
                enrolrepo.Save(enrolment);
                return true;
            }

            return false;
        }

        
        public string GenerateCertificate(int studentId, int courseId)
        {
            bool isCompleted = CheckIfCourseIsCompleted(studentId, courseId);
            if (!isCompleted) throw new Exception("Условията за завършване на курса не са покрити!");

            var course = courserepo.GetById(courseId);
            double finalGrade = studentsTestsService.CalculateCourseSuccessRate(studentId, courseId);

            // Генерираме уникален номер за документа
            string certNumber = $"CERT-{DateTime.Now.Year}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            return $"\n==================================================\n" +
                   $"               ОФИЦИАЛЕН СЕРТИФИКАТ                 \n" +
                   $"==================================================\n" +
                   $" Номер на документ: {certNumber}\n" +
                   $" Дата на издаване: {DateTime.Now:dd.MM.yyyy}\n\n" +
                   $" Идентификатор на студент: {studentId}\n" +
                   $" Курс: \"{course.Title}\"\n" +
                   $" Финална успеваемост в курса: {finalGrade:F2}%\n" +
                   $" Статус: ЗАВЪРШЕН УСПЕШНО 🎓\n" +
                   $"==================================================\n";
        }
        public List<Courses> GetActiveCoursesForStudent(int studentId)
        {
            
            var activeEnrolments = enrolrepo.GetAll()
                .Where(e => e.StudentId == studentId && e.Status.ToString() == "Active")
                .ToList();

           
            var activeCourses = new List<Courses>();
            foreach (var enrolment in activeEnrolments)
            {
                
                var course = courserepo.GetById(enrolment.CourseId);
                if (course != null)
                {
                    activeCourses.Add(course);
                }
            }

            return activeCourses;
        }
    }
}