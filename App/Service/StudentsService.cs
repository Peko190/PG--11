using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App
{
    public class StudentsService
    {
        private readonly IStudentsRepository studentsRepository;

        public StudentsService(IStudentsRepository studentsRepository)
        {
            this.studentsRepository = studentsRepository;
        }
        public void AddStudent(string firstname, string lastname, string email)  //int id, string firstname,string lastname,string email
        {

            var Student = new Students(
                0,
               firstname,
                lastname,
                email
                );

            studentsRepository.Save(Student);
        }
    }

}
