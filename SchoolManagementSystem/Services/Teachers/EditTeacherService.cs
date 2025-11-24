using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Teachers
{
    public class EditTeacherService
    {
        private readonly DataContext _context;
        public EditTeacherService(DataContext context)
        {
            _context = context;
        }

        public bool Edit(int id, string teacherCode, string firstName, string lastName, string college)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null)
                return false;

            teacher.TeacherCode = teacherCode;
            teacher.FirstName = firstName;
            teacher.LastName = lastName;
            teacher.College = college;

            return true;
        }
    }
}
