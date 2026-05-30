using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Domain.Entites
{
    public class Students
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Enrolments> Enrolments { get; set; } = new List<Enrolments>();

        public Students() { }
        public Students(int id, string firstname,string lastname,string email)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            
        }
    }
}
