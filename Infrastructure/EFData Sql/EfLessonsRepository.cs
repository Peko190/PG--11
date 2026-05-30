using PG_Тема_11.App;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class EfLessonsRepository : ILessonsRepository
    {
        private readonly AppDbContext context;
        public EfLessonsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IReadOnlyList<Lessons> GetAll()
        {
            return context.Lessons.ToList();
        }
        public void Save(Lessons lessons)
        {
            if (lessons == null)
            {
                throw new ArgumentNullException(nameof(lessons));
            }

            if (lessons.Id == 0)
            {
                context.Lessons.Add(lessons);
            }
            else
            {
                context.Lessons.Update(lessons);
            }

            context.SaveChanges();
        }

        public Lessons GetById(int id)
        {
            return context.Lessons.FirstOrDefault(c => c.Id == id);
        }
    }
}
