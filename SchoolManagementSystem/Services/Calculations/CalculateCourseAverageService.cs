using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Calculations
{
    public class CalculateCourseAverageService
    {
        private readonly DataContext _context;
        public CalculateCourseAverageService(DataContext context)
        {
            _context = context;
        }

        // Calculates average grade of a course
        public double? Calculate(int courseId)
        {
            var grades = _context.StudentCourses
                .Where(sc => sc.CourseId == courseId && sc.Grade.HasValue)
                .Select(sc => sc.Grade!.Value)
                .ToList();

            if (!grades.Any())
                return null;

            return grades.Average();
        }
    }
}
