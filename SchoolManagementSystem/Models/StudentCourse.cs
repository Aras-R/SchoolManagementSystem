using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }

        // Foreign keys
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        // Optional grade
        public int? Grade { get; set; }
    }
