using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Courses
{
    public class AssignGradeService
    {
        private readonly DataContext _context;
        public AssignGradeService(DataContext context)
        {
            _context = context;
        }
        public bool AssignGrade(int courseId, int studentId, int grade)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                throw new Exception("Course not found.");

            bool studentExists = course.Students.Any(s => s.Id == studentId);
            if (!studentExists)
                throw new Exception("Student is not assigned to this course.");

            if (grade < 0 || grade > 20)
                throw new Exception("Grade must be between 0 and 20.");

            course.Grades[studentId] = grade;

            return true;
        }
    }
}
