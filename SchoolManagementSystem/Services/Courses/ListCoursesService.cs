using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Courses
{
    // Returns all registered courses with their assigned teacher
    public class ListCoursesService
    {
        private readonly DataContext _context;

        public ListCoursesService(DataContext context)
        {
            _context = context;
        }

        // Gets all courses
        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }
    }
}
