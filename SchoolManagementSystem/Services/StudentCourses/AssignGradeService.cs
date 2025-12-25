using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.StudentCourses
{
    public class AssignGradeService
    {
        private readonly DataContext _context;

        public AssignGradeService(DataContext context)
        {
            _context = context;
        }

        // Assign or update a grade for a student in a course
        public StudentCourse Assign(int studentId, int courseId, int grade)
        {
            // Validate grade
            if (grade < 0 || grade > 20)
                throw new Exception("Grade must be between 0 and 20.");

            // Find the StudentCourse entry
            var studentCourse = _context.StudentCourses
                .FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (studentCourse == null)
                throw new Exception("Student is not registered in this course.");

            // Assign or update grade
            studentCourse.Grade = grade;

            return studentCourse;
        }
    }
}
