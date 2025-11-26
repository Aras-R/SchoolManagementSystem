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
            // Find teacher by ID
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null)
                return false;

            // Remove teacher from list
            _context.Teachers.Remove(teacher);
            return true;
        }
    }
}
