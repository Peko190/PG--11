using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Domain.Entites
{
    public class StudentTestResults
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public double Score { get; set; }

        public StudentTestResults() { }

        public StudentTestResults(int id, int studentId, int testId, double score)
        {
            Id = id;
            StudentId = studentId;
            TestId = testId;
            Score = score;
        }
    }
}
