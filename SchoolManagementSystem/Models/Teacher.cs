using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Models
{
    public class Teacher
    {
        //Defining teacher features
        public int Id { get; set; }                     
        public string TeacherCode { get; set; }         
        public string FirstName { get; set; }           
        public string LastName { get; set; }            
        public string College { get; set; }             

        public string FullName => $"{FirstName} {LastName}";

        public override string ToString()
        {
            return $"{Id} - {FullName} ({TeacherCode})";
        }

        // Returns full details of the teacher
        public string FullInfo()
        {
            return
                $"ID: {Id}\n" +
                $"Name: {FullName}\n" +
                $"Teacher Code: {TeacherCode}\n" +
                $"College: {College}\n";
        }

    }
}
