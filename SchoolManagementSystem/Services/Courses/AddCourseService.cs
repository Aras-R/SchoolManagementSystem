using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Courses
{
    public class AddCourseService
    {
        private readonly DataContext _context;

        public AddCourseService(DataContext context)
        {
            _context = context;
        }

        public Course Add(string title, int teacherId)
        {
            // Find assigned teacher
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher == null)
                throw new Exception("Teacher not found.");

            // Generate new course ID
            int newId = _context.Courses.Count == 0
                ? 1
                : _context.Courses.Max(c => c.Id) + 1;

            // Create course object
            var course = new Course
            {
                Id = newId,
                Title = title,
                Teacher = teacher,
            };

            // Save in list
            _context.Courses.Add(course);
            return course;
        }
    }
}
