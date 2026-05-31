using PG_Тема_11.App.Interface;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class EfProgressRepository : IProgressRepository
    {
        private readonly AppDbContext context; 

        public EfProgressRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IReadOnlyList<Progress> GetAll()
        {
            return context.Progress.ToList();
        }

        public Progress GetById(int id)
        {
            return context.Progress.FirstOrDefault(p => p.Id == id);
        }

        public void Save(Progress progress)
        {
            if (progress == null) throw new ArgumentNullException(nameof(progress));

            if (progress.Id == 0)
            {
                context.Progress.Add(progress);
            }
            else
            {
                context.Progress.Update(progress);
            }
            context.SaveChanges();
        }

        public void Delete(Progress progress)
        {
            if (progress != null)
            {
                context.Progress.Remove(progress);
                context.SaveChanges();
            }
        }
    }
}
