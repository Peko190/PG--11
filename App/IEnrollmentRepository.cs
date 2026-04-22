using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App
{
    public interface IEnrollmentRepository
    {
        void Save(Enrolments enrollment);
        
         IReadOnlyList<Enrolments> GetByAccountId(int id);


    }
}
