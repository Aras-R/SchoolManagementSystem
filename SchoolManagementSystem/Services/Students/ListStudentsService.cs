using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Students
{
    public class ListStudentsService
    {
        private readonly DataContext _Context;
        public ListStudentsService(DataContext dataContext)
        {
            _Context = dataContext;
        }

        public List<Student> GetAll()
        {
            // Return all students from list
            return _Context.Students;
        }
    }
}
