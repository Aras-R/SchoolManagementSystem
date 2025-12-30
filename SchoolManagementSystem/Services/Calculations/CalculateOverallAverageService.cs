using SchoolManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services.Calculations
{
    public class CalculateOverallAverageService
    {
        private readonly DataContext _context;
        public CalculateOverallAverageService(DataContext context)
        {
            _context = context;
        }

        // Calculates average grade of all students
        public double? Calculate()
        {
            var grades = _context.StudentCourses
                .Where(sc => sc.Grade.HasValue)
                .Select(sc => sc.Grade!.Value)
                .ToList();

            if (!grades.Any())
                return null;

            return grades.Average();
        }
    }
}
