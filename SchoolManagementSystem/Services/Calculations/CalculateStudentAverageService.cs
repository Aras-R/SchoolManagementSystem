using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Calculations
{
    public class CalculateStudentAverageService
    {
        private readonly DataContext _context;
        public CalculateStudentAverageService(DataContext context)
        {
            _context = context;
        }

        // Calculates average grade of a student
        public double? Calculate(int studentId)
        {
            var grades = _context.StudentCourses
                .Where(sc => sc.StudentId == studentId && sc.Grade.HasValue)
                .Select(sc => sc.Grade!.Value)
                .ToList();

            if (!grades.Any())
                return null;

            return grades.Average();
        }
    }
}
