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
            this.lessonRepo = lessonRepo;
        }
        public void AddLessons(string title, string content, int courseId)
        {

            var lesson = new Lessons(
                0,
               title,
                content,
                0,
                courseId
                );

            lessonRepo.Save(lesson);
        }
        public void EditLesson(int id,string title, string content)
        {
            var lesson = lessonRepo.GetById(id);
            if (lesson == null)
            {
                throw new Exception("Lesson not found");
            }
            lesson.Title = title;
            lesson.Content = content;
            lessonRepo.Save(lesson);
           
        }
    }
}
