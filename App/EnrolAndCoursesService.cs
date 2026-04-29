using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public EnrolAndCoursesService(IEnrollmentRepository enrolrepo, ICourseRepository courserepo)
        {
            this.courserepo = courserepo;
            this.enrolrepo = enrolrepo;
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


    }
}
