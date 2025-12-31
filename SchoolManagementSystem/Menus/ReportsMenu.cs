using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Menus
{
    public class ReportsMenu
    {
        private readonly DataContext _context;

        public ReportsMenu(DataContext context)
        {
            _context = context;
        }

        // Shows reports menu (averages & statistics)
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Reports Menu ===");
                Console.WriteLine("----------------------------");
                Console.WriteLine("1) Student Average");
                Console.WriteLine("2) Course Average");
                Console.WriteLine("3) Overall Average");
                Console.WriteLine("----------------------------");
                Console.WriteLine("0) Back");
                Console.WriteLine("----------------------------");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": ShowStudentAverage(); break;
                    case "2": ShowCourseAverage(); break;
                    case "3": ShowOverallAverage(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid choice!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //------------------------------------------
        // Shows average grade of a selected student
        private void ShowStudentAverage()
        {
            Console.Clear();
            Console.WriteLine("=== Student Average ===");
            Console.WriteLine("-----------------------");

            if (!_context.Students.Any())
            {
                Console.WriteLine("No students found!");
                Console.ReadKey();
                return;
            }

            int studentId = SelectStudentId();
            if (studentId == -1) return; // Invalid or no students

            var service = new CalculateStudentAverageService(_context);
            var average = service.Calculate(studentId);

            Console.WriteLine(
                average == null
                    ? "No grades found for this student."
                    : $"Student Average: {average:F2}"
            );

            Console.ReadKey();
        }
        //------------------------------------------
        // Shows average grade of a selected course
        private void ShowCourseAverage()
        {
            Console.Clear();
            Console.WriteLine("=== Course Average ===");
            Console.WriteLine("----------------------");

            if (!_context.Courses.Any())
            {
                Console.WriteLine("No courses found!");
                Console.ReadKey();
                return;
            }

            int courseId = SelectCourseId();
            if (courseId == -1) return; // Invalid or no courses

            var service = new CalculateCourseAverageService(_context);
            var average = service.Calculate(courseId);

            Console.WriteLine(
                average == null
                    ? "No grades found for this course."
                    : $"Course Average: {average:F2}"
            );

            Console.ReadKey();
        }
        //------------------------------------------
        // Shows overall average of all grades
        private void ShowOverallAverage()
        {
            Console.Clear();
            Console.WriteLine("=== Overall Average ===");
            Console.WriteLine("-----------------------");

            var service = new CalculateOverallAverageService(_context);
            var average = service.Calculate();

            Console.WriteLine(
                average == null
                    ? "No grades found in system."
                    : $"Overall Average: {average:F2}"
            );

            Console.ReadKey();
        }

        // -----------------------
        // Helper Methods
        // Displays students and reads valid student ID
        private int SelectStudentId()
        {
            Console.WriteLine("Available Students:");
            foreach (var s in _context.Students)
                Console.WriteLine($"{s.Id} - {s.FullName}");

            while (true)
            {
                Console.Write("Student Id: ");
                if (int.TryParse(Console.ReadLine(), out int id) && _context.Students.Any(s => s.Id == id))
                    return id;

                Console.WriteLine("Invalid Student Id! Press any key to return...");
                Console.ReadKey();
                return -1;
            }
        }

        // Displays courses and reads valid course ID
        private int SelectCourseId()
        {
            Console.WriteLine("Available Courses:");
            foreach (var c in _context.Courses)
                Console.WriteLine($"{c.Id} - {c.Title}");

            while (true)
            {
                Console.Write("Course Id: ");
                if (int.TryParse(Console.ReadLine(), out int id) && _context.Courses.Any(c => c.Id == id))
                    return id;

                Console.WriteLine("Invalid Course Id! Press any key to return...");
                Console.ReadKey();
                return -1;
            }
        }
    }
}
