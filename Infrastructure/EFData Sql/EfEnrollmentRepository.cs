using PG_Тема_11.App;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Save(Enrolments enrollment)
        {
            if (enrollment == null)
                throw new ArgumentNullException(nameof(enrollment));

            context.Enrolments.Add(enrollment);
            context.SaveChanges();
        }

        public IReadOnlyList<Enrolments> GetById(int id)
        {
            return context.Enrolments
                .Where(e => e.CourseId == id)
                .ToList();
        }
    }
}
