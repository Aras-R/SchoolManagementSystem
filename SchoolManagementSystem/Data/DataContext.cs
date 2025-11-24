using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Data
{
    public class DataContext
    {
        public List<Student> Students { get; set; } = new();
        public List<Teacher> Teachers { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
    }
}
