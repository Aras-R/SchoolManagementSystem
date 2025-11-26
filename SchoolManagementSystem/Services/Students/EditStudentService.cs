using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Students
{
    public class EditStudentService
    {
        private readonly DataContext _Context;
        public EditStudentService(DataContext Context)
        {
            _Context = Context;
        }

        public Student? Edit(int id, string firstName, string lastName, string studentNumber, DateTime birthDate, string major)
        {
            // Find student by ID
            var student = _Context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return null;

            // Update fields
            student.FirstName = firstName;
            student.LastName = lastName;
            student.StudentNumber = studentNumber;
            student.BirthDate = birthDate;
            student.Major = major;

            return student;
        }
    }
}
