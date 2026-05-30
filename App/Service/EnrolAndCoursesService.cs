using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App
{
    public class EnrolAndCoursesService
    {

        private readonly ICourseRepository courserepo;
        private readonly IEnrollmentRepository enrolrepo;
        private readonly ILessonsRepository lessonrepo;

        public EnrolAndCoursesService(IEnrollmentRepository enrolrepo, ICourseRepository courserepo,ILessonsRepository lessonsrepo)
        {
            this.courserepo = courserepo;
            this.enrolrepo = enrolrepo;
            this.lessonrepo = lessonsrepo;
        }
        
        public void CreateCourse(string title,string description,Level level)
        {
           var course = new Courses(
               0,
               title,
               description, 
               level);

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
            course.level= level;
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

            int currentEnrolmentsCount = enrolrepo.GetAll()
                .Count(e => e.CourseId == courseId);

           
            bool alreadyEnrolled = enrolrepo.GetAll()
                .Any(e => e.StudentId == studentId && e.CourseId == courseId);

            if (alreadyEnrolled)
            {
                throw new Exception("Студентът вече е записан за този курс!");
            }

            
            var newEnrolment = new Enrolments(
                0,
                studentId,
                courseId,
                EnrollmentStatus.Active,
                0.0,
                DateTime.Now 
            );
            
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
        public void CompleteLesson(int studentId, int courseId, int lessonId)
        {
            
            var enrolment = enrolrepo.GetAll()
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrolment == null)
            {
                throw new Exception("Обучаемият не е записан в този курс!");
            }

            
            int totalLessons = lessonrepo.GetAll()
                .Count(l => l.CourseId == courseId);

            if (totalLessons == 0)
            {
                throw new Exception("Този курс все още няма добавени уроци.");
            }

           
            double progressPerLesson = 100.0 / totalLessons;

            if (enrolment.Progress + progressPerLesson > 100)
            {
                enrolment.Progress = 100;
            }
            else
            {
                enrolment.Progress += progressPerLesson;
            }

            if (enrolment.Progress >= 100)
            {
                enrolment.Status = EnrollmentStatus.Completed; 
            }

            
            enrolrepo.Save(enrolment);
        }

        
        public Enrolments GetStudentProgress(int studentId, int courseId)
        {
            var enrolment = enrolrepo.GetAll()
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrolment == null)
            {
                throw new Exception("Обучаемият не е записан в този курс или курсът не съществува.");
            }

            return enrolment;
        }
    }
}
