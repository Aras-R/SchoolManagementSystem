using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.StudentCourses
{
    public class DeleteStudentFromCourseService
    {
        private readonly DataContext _context;

        public DeleteStudentFromCourseService(DataContext context)
        {
            _context = context;
        }

        // Remove a student from a course
        public bool Delete(int studentId, int courseId)
        {
            // Find the StudentCourse entry
            var studentCourse = _context.StudentCourses
                .FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (studentCourse == null)
                return false; 

            
            _context.StudentCourses.Remove(studentCourse);
            return true;
        }
    }
}
