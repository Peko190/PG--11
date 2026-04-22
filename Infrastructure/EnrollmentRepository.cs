using PG_Тема_11.App;
using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly Storage storage;

        public EnrollmentRepository(Storage storage)
        {
            this.storage = storage;
        }

        public void Save(Enrolments enrolment)
        {
            var db = storage.Load();
            var newEnrol = new Enrolments(
                    db.NextId++,
                    enrolment.StudentId,
                    enrolment.CourseId,
                    enrolment.Status,
                    enrolment.Progress
                    
                    );
            db.Enrolments.Add(newEnrol);

            storage.Save(db);
        }

        public IReadOnlyList<Enrolments> GetByAccountId(int id)
        {
            var db = storage.Load();

            var result = new List<Enrolments>();

            foreach (var enrol in db.Enrolments)
            {
                if (enrol.Id== id)
                    result.Add(enrol);
            }
            return result;
        }
    }
}

