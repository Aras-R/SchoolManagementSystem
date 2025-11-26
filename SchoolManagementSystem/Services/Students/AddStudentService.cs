using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Students
{
    public class AddStudentService
    {
        private readonly DataContext _context;
        public AddStudentService(DataContext context)
        {
            _context = context;
        }

        public Student Add(string firstName, string lastName, string studentNumber, DateTime birthDate, string major)
        {
            // Create new ID (if no students → start from 1)
            int newId = _context.Students.Count == 0
                ? 1
                : _context.Students.Max(s => s.Id) + 1;

            // Create student object
            var student = new Student
            {
                Id = newId,
                FirstName = firstName,
                LastName = lastName,
                StudentNumber = studentNumber,
                BirthDate = birthDate,
                Major = major
            };

            // Add to database (list)
            _context.Students.Add(student);

            return student;
        }
    }
}
