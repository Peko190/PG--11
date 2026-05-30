using PG_Тема_11.App;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class EfStudentsRepository : IStudentsRepository
    {

        private readonly AppDbContext context;
        public EfStudentsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IReadOnlyList<Students> GetAll()
        {
            return context.Students.ToList();
        }
        public void Save(Students students)
        {
            if (students == null)
            {
                throw new ArgumentNullException(nameof(students));
            }

            if (students.Id == 0)
            {
                context.Students.Add(students);
            }
            else
            {
                context.Students.Update(students);
            }

            context.SaveChanges();
        }

        public Students GetById(int id)
        {
            return context.Students.FirstOrDefault(c => c.Id == id);
        }
    }
}
