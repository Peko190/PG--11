using PG_Тема_11.App;
using PG_Тема_11.App.Interface;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class EfTestsRepository : ITestsRepository
    {
       
            private readonly AppDbContext context;
            public EfTestsRepository(AppDbContext context)
            {
                this.context = context;
            }
            public IReadOnlyList<Tests> GetAll()
            {
                return context.Tests.ToList();
            }
            public void Save(Tests tests)
            {
                if (tests == null)
                {
                    throw new ArgumentNullException(nameof(tests));
                }

                if (tests.Id == 0)
                {
                    context.Tests.Add(tests);
                }
                else
                {
                    context.Tests.Update(tests);
                }

                context.SaveChanges();
            }

            public Tests GetById(int id)
            {
                return context.Tests.FirstOrDefault(c => c.Id == id);
            }
        
    }
}
