using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Students
{
    public class GetStudentByIdService
    {
        private readonly DataContext _Context;
        public GetStudentByIdService(DataContext context)
        {
            _Context = context;
        }

        public Student? Get(int id)
        {
            return _Context.Students.FirstOrDefault(s => s.Id == id);
        }
    }
}
