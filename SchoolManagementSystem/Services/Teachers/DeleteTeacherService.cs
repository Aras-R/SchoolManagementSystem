using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Teachers
{
    public class DeleteTeacherService
    {
        private readonly DataContext _context;

        public DeleteTeacherService(DataContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null) return false;

            // Optional: remove or unassign courses taught by this teacher
            foreach (var course in _context.Courses.Where(c => c.Teacher.Id == id))
            {
                course.Teacher = null; // or remove the course if desired
            }

            _context.Teachers.Remove(teacher);
            return true;
        }
    }
}
