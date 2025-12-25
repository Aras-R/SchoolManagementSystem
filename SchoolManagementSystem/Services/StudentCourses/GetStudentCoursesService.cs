using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.StudentCourses
{
    public class GetStudentCoursesService
    {
        private readonly DataContext _context;

        public GetStudentCoursesService(DataContext context)
        {
            _context = context;
        }

        // Get all courses of a student with their grades
        public List<CourseWithGrade> Get(int studentId)
        {
            // Find all StudentCourse entries for this student
            var studentCourses = _context.StudentCourses
                .Where(sc => sc.StudentId == studentId)
                .ToList();

            var result = new List<CourseWithGrade>();

            foreach (var sc in studentCourses)
            {
                var course = _context.Courses.FirstOrDefault(c => c.Id == sc.CourseId);
                if (course != null)
                {
                    result.Add(new CourseWithGrade
                    {
                        Course = course,
                        Grade = sc.Grade
                    });
                }
            }

            return result;
        }
    }

    // Helper class to return course + grade
    public class CourseWithGrade
    {
        public Course Course { get; set; }
        public int? Grade { get; set; }
    }
}
