using PG_Тема_11.App;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class EfCourseRepository : ICourseRepository
    {
        private readonly AppDbContext context;

        public EfCourseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Courses GetById(int id)
        {
            return context.Courses.FirstOrDefault(c => c.Id == id);
        }

        public void Save(Courses course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            if (course.Id == 0)
            {
                context.Courses.Add(course);
            }
            else
            {
                context.Courses.Update(course);
            }

            context.SaveChanges();
        }
        public IReadOnlyList<Courses> GetAll()
        {
            return context.Courses.ToList();
        }
    }
}
