using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App.Interface
{
    public interface IStudentsTest
    {
        StudentTestResults GetById(int id);
        IReadOnlyList<StudentTestResults> GetAll();
        void Save(StudentTestResults studentTestResults);
    }
}
    