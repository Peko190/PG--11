using PG_Тема_11.Domain.Entites;
using PG_Тема_11.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.App
{
    public class EnrolmentService : IEnrollmentRepository
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;

        public EnrolmentService(ICourseRepository courseRepo, IEnrollmentRepository enrollmentRepo)
        {
            _courseRepo = courseRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public void Enroll(int studentId, int courseId)
        {
            var course = _courseRepo.GetById(courseId);

            if (course == null)
                throw new Exception("Course not found");

           

            var enrollment = new Enrolments
            {
                StudentId = studentId,
                CourseId = courseId,
                Status = EnrollmentStatus.Active
            };

            _enrollmentRepo.Add(enrollment);

          
            _courseRepo.Update(course);
        }
    }
}
