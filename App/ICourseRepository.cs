using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App
{
    public interface ICourseRepository
    {
        Courses GetById(int id);
        
        void Save(Courses course);

        IReadOnlyList<Courses> GetAll();
    }
}
