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
        // List of all students in the system.
        public List<Student> Students { get; set; } = new();

        // List of all teachers in the system.
        public List<Teacher> Teachers { get; set; } = new();

        // List of all courses in the system.
        public List<Course> Courses { get; set; } = new();

        // List of student-course relationships
        public List<StudentCourse> StudentCourses { get; set; } = new();
    }
}
