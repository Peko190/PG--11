using PG_Тема_11.App;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly Storage storage;

        public LessonsRepository(Storage storage)
        {
            this.storage = storage;
        }
        public void Save(Lessons lessons)
        {
            var db = storage.Load();
            var newLesson = new Lessons(
                    db.NextId++,
                    lessons.Title,
                    lessons.Content,
                    lessons.Order,
                    lessons.CourseId

                    );
            db.Lessons.Add( newLesson );

            storage.Save(db);
        }
        public IReadOnlyList<Lessons> GetAll()
        {
            var db = storage.Load();
            return db.Lessons;
        }

    }
}
