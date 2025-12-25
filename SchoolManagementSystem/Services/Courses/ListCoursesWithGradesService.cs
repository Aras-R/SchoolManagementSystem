using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Courses
{
    public class ListCoursesWithGradesService
    {
        private readonly DataContext _context;

        public ListCoursesWithGradesService(DataContext context)
        {
            _context = context;
        }

        // Get all courses with enrolled students and their grades
        public List<CourseWithStudentsAndGrades> GetAll()
        {
            var result = new List<CourseWithStudentsAndGrades>();

            foreach (var course in _context.Courses)
            {
                // Find all StudentCourse entries for this course
                var studentCourses = _context.StudentCourses
                    .Where(sc => sc.CourseId == course.Id)
                    .ToList();

                var studentsWithGrades = studentCourses
                    .Select(sc =>
                    {
                        var student = _context.Students.FirstOrDefault(s => s.Id == sc.StudentId);
                        return new StudentGrade
                        {
                            Student = student!,
                            Grade = sc.Grade
                        };
                    }).ToList();

                result.Add(new CourseWithStudentsAndGrades
                {
                    Course = course,
                    StudentsWithGrades = studentsWithGrades
                });
            }

            return result;
        }
    }

    // Helper class to return student + grade
    public class StudentGrade
    {
        public Student Student { get; set; }
        public int? Grade { get; set; }
    }

    // Helper class to return course + its students + grades
    public class CourseWithStudentsAndGrades
    {
        public Course Course { get; set; }
        public List<StudentGrade> StudentsWithGrades { get; set; } = new();
    }
}
