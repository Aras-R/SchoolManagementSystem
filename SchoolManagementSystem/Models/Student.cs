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

        public override string ToString()
        {
            return $"{Id} - {FirstName} {LastName} - ({StudentNumber})";
        }
    }
}
