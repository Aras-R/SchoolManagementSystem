using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Menus
{
    public class CourseMenu
    {

        private readonly DataContext _context;

        public CourseMenu(DataContext context)
        {
            _context = context;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Course Management ===");
                Console.WriteLine("1) Add Course");
                Console.WriteLine("2) Add Student to Course");
                Console.WriteLine("3) Assign Grade to Student");
                Console.WriteLine("4) List Courses with Students and Grades");
                Console.WriteLine("0) Back");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddCourse();
                        break;

                    case "2":
                        AddStudentToCourse();
                        break;

                    case "3":
                        AssignGrade();
                        break;

                    case "4":
                        ListCourses();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Course ===");

            Console.Write("Course Title: ");
            string title = Console.ReadLine()!;

            // نمایش استادها
            Console.WriteLine("\nAvailable Teachers:");
            foreach (var t in _context.Teachers)
                Console.WriteLine($"{t.Id} - {t.FullName}");

            Console.Write("\nSelect Teacher Id: ");
            int teacherId = int.Parse(Console.ReadLine()!);

            var service = new AddCourseService(_context);
            var course = service.Add(title, teacherId);

            Console.WriteLine($"\nCourse created: {course.Title} (Teacher: {course.Teacher.FullName})");
            Console.ReadKey();
        }

        private void AddStudentToCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Student to Course ===");

            Console.WriteLine("Available Courses:");
            foreach (var c in _context.Courses)
                Console.WriteLine($"{c.Id} - {c.Title}");

            Console.Write("\nEnter Course Id: ");
            int courseId = int.Parse(Console.ReadLine()!);

            Console.WriteLine("\nAvailable Students:");
            foreach (var s in _context.Students)
                Console.WriteLine($"{s.Id} - {s.FullName}");

            Console.Write("\nEnter Student Id: ");
            int studentId = int.Parse(Console.ReadLine()!);

            var service = new AddStudentToCourseService(_context);

            try
            {
                service.AddStudent(courseId, studentId);
                Console.WriteLine("\nStudent successfully added to course!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.ReadKey();
        }

        private void AssignGrade()
        {
            Console.Clear();
            Console.WriteLine("=== Assign Grade ===");

            Console.WriteLine("Available Courses:");
            foreach (var c in _context.Courses)
                Console.WriteLine($"{c.Id} - {c.Title}");

            Console.Write("\nEnter Course Id: ");
            int courseId = int.Parse(Console.ReadLine()!);

            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                Console.ReadKey();
                return;
            }

            if (course.Students.Count == 0)
            {
                Console.WriteLine("This course has no students!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nStudents in Course:");
            foreach (var s in course.Students)
                Console.WriteLine($"{s.Id} - {s.FullName}");

            Console.Write("\nEnter Student Id: ");
            int studentId = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Grade (0-20): ");
            int grade = int.Parse(Console.ReadLine()!);

            var service = new AssignGradeService(_context);

            try
            {
                service.AssignGrade(courseId, studentId, grade);
                Console.WriteLine("\nGrade assigned successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.ReadKey();
        }

        private void ListCourses()
        {
            Console.Clear();
            Console.WriteLine("=== Courses with Students and Grades ===");

            var service = new ListCoursesWithGradesService(_context);
            var courses = service.GetAll();

            if (courses.Count == 0)
            {
                Console.WriteLine("No courses found.");
                Console.ReadKey();
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"\nCourse: {course.Title}");
                Console.WriteLine($"Teacher: {course.Teacher.FullName}");

                if (course.Students.Count == 0)
                {
                    Console.WriteLine("No students in this course.");
                    continue;
                }

                Console.WriteLine("\nStudents:");

                foreach (var student in course.Students)
                {
                    string gradeDisplay = course.Grades.ContainsKey(student.Id)
                        ? course.Grades[student.Id].ToString()
                        : "No Grade";

                    Console.WriteLine($"- {student.FullName} → Grade: {gradeDisplay}");
                }

                Console.WriteLine("-----------------------------");
            }

            Console.ReadKey();
        }
    }
}
