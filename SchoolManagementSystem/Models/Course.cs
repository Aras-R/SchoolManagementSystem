using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // Assigned teacher for the course
        public Teacher Teacher { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Title} (Teacher: {Teacher?.FullName ?? "No Teacher"})";
        }
    }
}
