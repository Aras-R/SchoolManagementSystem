using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Teachers
{
    public class GetTeacherByIdService
    {
        private readonly DataContext _context;
        public GetTeacherByIdService(DataContext context)
        {
            _context = context;
        }

        public Teacher? GetById(int id)
        {
            return _context.Teachers.FirstOrDefault(t => t.Id == id);
        }
    }
}
