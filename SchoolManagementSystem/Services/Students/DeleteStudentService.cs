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
        private readonly DataContext _Context;
        public DeleteStudentService(DataContext context)
        {
            _Context = context;
        }

        public bool Delete(int id)
        {
            var student = _Context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return false;
            }

            _Context.Students.Remove(student);
            return true;
        }
    }
}
