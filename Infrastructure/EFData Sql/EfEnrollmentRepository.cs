using PG_Тема_11.App;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class EfEnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext context;

        public EfEnrollmentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Save(Enrolments enrolment)
        {
            if (enrolment == null)
            {
                throw new ArgumentNullException(nameof(enrolment));
            }

           
            if (enrolment.Id == 0)
            {
                context.Enrolments.Add(enrolment);
            }
            
            else
            {
                context.Enrolments.Update(enrolment);
            }

            context.SaveChanges();
        }

        public IReadOnlyList<Enrolments> GetById(int id)
        {
            return context.Enrolments
                .Include(e => e.Student)
                .Where(e => e.CourseId == id)
                .ToList();
        }
        public IReadOnlyList<Enrolments> GetAll()
        {
            return context.Enrolments.ToList();
        }
        
    }
}
