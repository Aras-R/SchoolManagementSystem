using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.StudentCourses
{
    public class AddStudentToCourseService
    {
        private readonly DataContext _context;

        public AddStudentToCourseService(DataContext context)
        {
            _context = context;
        }

        // Add a student to a course
        public StudentCourse Add(int studentId, int courseId)
        {
            // Find the student
            var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
                throw new Exception("Student not found.");

            // Find the course
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                throw new Exception("Course not found.");

            // Check if the student is already registered
            bool alreadyExists = _context.StudentCourses
                .Any(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if (alreadyExists)
                throw new Exception("Student already registered to this course.");

            // Generate new ID for StudentCourse
            int newId = _context.StudentCourses.Count == 0
                ? 1
                : _context.StudentCourses.Max(sc => sc.Id) + 1;

            // Create StudentCourse entry
            var studentCourse = new StudentCourse
            {
                Id = newId,
                StudentId = studentId,
                CourseId = courseId,
                Grade = null // grade not assigned yet
            };

            // Add to context
            _context.StudentCourses.Add(studentCourse);

            return studentCourse;
        }
    }
        
}
