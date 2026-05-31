using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PG_Тема_11.Domain.Entites
{
    public class TestQuestions
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string CorrectAnswer { get; set; }

        public TestQuestions()
        {
        }
        public TestQuestions(int id, int testId, string text, string a, string b, string c, string correct)
        {
            Id = id;
            TestId = testId;
            QuestionText = text;
            OptionA = a;
            OptionB = b;
            OptionC = c;
            CorrectAnswer = correct;
        }
    }
}
