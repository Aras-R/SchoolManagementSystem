using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Teachers
{
    public class ListTeachersService
    {
        private readonly DataContext _Context;  
        public ListTeachersService(DataContext context)
        {
            _Context = context;
        }

        public List<Teacher> GetAll()
        {
            // Return all teachers in list
            return _Context.Teachers.ToList();
        }
    }
}
