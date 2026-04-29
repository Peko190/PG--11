using PG_Тема_11.App;
using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
       private readonly Storage storage;

        public CourseRepository(Storage storage)
        {
            this.storage = storage;
        }
        public IReadOnlyList<Courses> GetAll()
        {
            var db = storage.Load();
            return db.Courses;
        }
       

        public Courses GetById(int id)
        {
            var db = storage.Load();

            foreach (var course in db.Courses)
            {
                if (course.Id == id)
                {
                    return course;
                }
            }

            throw new Exception("Course not found");
        }
        public void Save(Courses course)
        {
            var db = storage.Load();

            if (course.Id == 0)
            {
                var newCourse = new Courses(
             
                db.NextId++,
                    course.Title,
                    course.Description,
                    course.level
                    );
                db.Courses.Add(newCourse);
            }
           

            storage.Save(db);

        }
    }

}
