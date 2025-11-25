using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Courses
{
    public class ListCoursesWithGradesService
    {
        private readonly DataContext _context;
        public ListCoursesWithGradesService(DataContext context)
        {
            _context = context;
        }

        public List<Course> GetAll()
        {
            return _context.Courses;
        }
    }
}
