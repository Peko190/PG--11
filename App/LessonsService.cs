using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App
{
    public class LessonsService
    {
        private readonly ILessonsRepository lessonRepo;
        public LessonsService(ILessonsRepository lessonRepo) 
        {
        }
        public void AddLessons(string title, string content, int order)
        {

            var lesson = new Lessons(
                0,
               title,
                content,
                order,
                0
                );

            lessonRepo.Save(lesson);
        }
    }
}
