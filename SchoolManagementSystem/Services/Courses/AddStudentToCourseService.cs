using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Courses
{
    public class AddStudentToCourseService
    {
        private readonly DataContext _context;
        public AddStudentToCourseService(DataContext context)
        {
            _context = context;
        }

        public bool AddStudent(int courseId, int studentId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            var  student = _context.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                throw new Exception("Student not found.");
            }

            bool alreadyExists = course.Students.Any(s => s.Id == studentId);
            if (alreadyExists)
            {
                throw new Exception("Student already assigned to this course.");
            }

            course.Students.Add(student);
            return true;
        }
    }
}
