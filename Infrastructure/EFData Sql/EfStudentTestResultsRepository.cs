using PG_Тема_11.App.Interface;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class EfStudentTestResultsRepository : IStudentsTest
    {

        private readonly AppDbContext context; 

        public EfStudentTestResultsRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IReadOnlyList<StudentTestResults> GetAll()
        {
            return context.StudentTestResults.ToList();
        }

        public StudentTestResults GetById(int id)
        {
            return context.StudentTestResults.FirstOrDefault(r => r.Id == id);
        }

        public void Save(StudentTestResults result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            
            if (result.Id == 0)
            {
                context.StudentTestResults.Add(result);
            }
            
            else
            {
                context.StudentTestResults.Update(result);
            }

            context.SaveChanges();
        }
    }
}

