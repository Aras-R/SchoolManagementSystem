using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Courses
{
    public class EditCourseService
    {
        private readonly DataContext _context;

        public EditCourseService(DataContext context)
        {
            _context = context;
        }

        public bool Edit(int courseId, string newTitle, int newTeacherId)
        {
            // Find course
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                throw new Exception("Course not found.");

            // Find teacher
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == newTeacherId);
            if (teacher == null)
                throw new Exception("Teacher not found.");

            // Update fields
            course.Title = newTitle;
            course.Teacher = teacher;
            return true;
        }
    }
}
