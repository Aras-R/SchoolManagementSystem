using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Students
{
    public class DeleteStudentService
    {
        private readonly DataContext _context;
        public DeleteStudentService(DataContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return false;

            // Remove all related StudentCourse entries
            _context.StudentCourses.RemoveAll(sc => sc.StudentId == id);

            _context.Students.Remove(student);
            return true;
        }

    }
}
