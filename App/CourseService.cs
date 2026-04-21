using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App
{
    public class CourseService : ICourseRepository
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public Courses CreateCourse(Courses course)
        {
            _repo.Add(course);
            return course;
        }

        public List<Courses> GetCourses()
        {
            return _repo.GetAll();
        }
    }
}
