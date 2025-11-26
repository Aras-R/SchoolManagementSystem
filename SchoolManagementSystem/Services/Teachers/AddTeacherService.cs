using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Teachers
{
    public class AddTeacherService
    {
        private readonly DataContext _context;
        public AddTeacherService(DataContext context)
        {
            _context = context;
        }

        public Teacher Add(string teacherCode, string firstName, string lastName, string college)
        {
            // Create new ID 
            int newId = _context.Teachers.Count == 0
                ? 1
                : _context.Teachers.Max(t => t.Id) + 1;

            // Create teacher object
            var teacher = new Teacher
            {
                Id = newId,
                TeacherCode = teacherCode,
                FirstName = firstName,
                LastName = lastName,
                College = college
            };

            // Add to list
            _context.Teachers.Add(teacher);
            return teacher;

        }
    }
}
