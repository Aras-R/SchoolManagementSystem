using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.StudentCourses
{
    public class GetCourseStudentsService
    {
        private readonly DataContext _context;

        public GetCourseStudentsService(DataContext context)
        {
            _context = context;
        }

        // Get all students of a course with their grades
        public List<StudentWithGrade> Get(int courseId)
        {
            // Find all StudentCourse entries for this course
            var studentCourses = _context.StudentCourses
                .Where(sc => sc.CourseId == courseId)
                .ToList();

            var result = new List<StudentWithGrade>();

            foreach (var sc in studentCourses)
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == sc.StudentId);
                if (student != null)
                {
                    result.Add(new StudentWithGrade
                    {
                        Student = student,
                        Grade = sc.Grade
                    });
                }
            }

            return result;
        }
    }

    // Helper class to return student + grade
    public class StudentWithGrade
    {
        public Student Student { get; set; }
        public int? Grade { get; set; }
    }
}
