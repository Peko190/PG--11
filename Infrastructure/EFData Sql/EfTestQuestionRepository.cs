using PG_Тема_11.App.Interface;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class EfTestQuestionRepository : ITestQuestionsRepository
    {
        private readonly AppDbContext context;

        public EfTestQuestionRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IReadOnlyList<TestQuestions> GetAll()
        {
            return context.TestQuestions.ToList();
        }

        public TestQuestions GetById(int id)
        {
            return context.TestQuestions.FirstOrDefault(q => q.Id == id);
        }

        public void Save(TestQuestions question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            
            if (question.Id == 0)
            {
                context.TestQuestions.Add(question);
            }
            
            else
            {
                context.TestQuestions.Update(question);
            }

            context.SaveChanges();
        }

        public void Delete(TestQuestions question)
        {
            if (question != null)
            {
                context.TestQuestions.Remove(question);
                context.SaveChanges();
            }
        }
    }
}
