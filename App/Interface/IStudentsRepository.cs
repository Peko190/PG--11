using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App
{
    public interface IStudentsRepository
    {
        Students GetById(int id);
        IReadOnlyList<Students> GetAll();
        void Save(Students students);
    }
}
