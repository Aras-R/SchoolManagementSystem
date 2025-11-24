using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Major { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public override string ToString()
        {
            return $"{Id} - {FullName} ({StudentNumber})";
        }

        public string FullInfo()
        {
            return
                $"ID: {Id}\n" +
                $"Name: {FullName}\n" +
                $"Student Number: {StudentNumber}\n" +
                $"Birth Date: {BirthDate:yyyy-MM-dd}\n" +
                $"Major: {Major}\n";
        }

    }
}  

