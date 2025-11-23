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

        public Teacher Teacher { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

        public Dictionary<int, int> Grades { get; set; } = new Dictionary<int, int>();

        public override string ToString()
        {
            return $"{Id} - {Title} (Teacher: {Teacher?.FullName ?? "No Teacher"})";
        }
    }
}
