using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Courses
{
    public class DeleteCourseService
    {
        private readonly DataContext _context;
        public DeleteCourseService(DataContext context)
        {
            _context = context;
        }

        public bool Delete(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            return true;
        }
    }
}
