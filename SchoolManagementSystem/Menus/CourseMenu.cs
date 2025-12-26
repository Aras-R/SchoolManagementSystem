using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services.Courses;
using SchoolManagementSystem.Services.StudentCourses;
using System;
using System.Linq;

namespace SchoolManagementSystem.Menus
{
    public class CourseMenu
    {
        private readonly DataContext _context;

        public CourseMenu(DataContext context)
        {
            _context = context;
        }

        // Shows the course management menu.
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Course Management ===");
                Console.WriteLine("1) Add Course");
                Console.WriteLine("2) Edit Course");
                Console.WriteLine("3) Delete Course");
                Console.WriteLine("4) List Courses");
                Console.WriteLine("5) View Course Students");
                Console.WriteLine("0) Back");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": AddCourse(); break;
                    case "2": EditCourse(); break;
                    case "3": DeleteCourse(); break;
                    case "4": ListCourses(); break;
                    case "5": ViewCourseStudents(); break;
                    case "0": return;

                    default:
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ---------------------------------------------------------
        // Course Operations

        private void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Course ===");

            if (!_context.Teachers.Any())
            {
                Console.WriteLine("No teachers found. Please add a teacher first.");
                Console.ReadKey();
                return;
            }

            string title = ReadCourseTitle();
            int teacherId = ReadTeacherId();

            var service = new AddCourseService(_context);
            var course = service.Add(title, teacherId);

            Console.WriteLine($"\nCourse created: {course.Title}");
            Console.ReadKey();
        }

        private void EditCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Course ===");

            int courseId = ReadCourseId();

            string newTitle = ReadCourseTitle();
            int newTeacherId = ReadTeacherId();

            var service = new EditCourseService(_context);
            service.Edit(courseId, newTitle, newTeacherId);

            Console.WriteLine("Course updated successfully!");
            Console.ReadKey();
        }

        private void DeleteCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Course ===");

            int courseId = ReadCourseId();

            var service = new DeleteCourseService(_context);
            bool result = service.Delete(courseId);

            Console.WriteLine(result ? "Course deleted successfully!" : "Course not found!");
            Console.ReadKey();
        }

        private void ListCourses()
        {
            Console.Clear();
            Console.WriteLine("=== Course List ===");

            var service = new ListCoursesService(_context);
            var courses = service.GetAll();

            if (!courses.Any())
            {
                Console.WriteLine("No courses found.");
            }
            else
            {
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course.Id} - {course.Title} (Teacher: {course.Teacher.FullName})");
                }
            }

            Console.ReadKey();
        }

        private void ViewCourseStudents()
        {
            Console.Clear();
            Console.WriteLine("=== Course Students ===");

            int courseId = ReadCourseId();

            var service = new GetCourseStudentsService(_context);
            var students = service.Get(courseId);

            if (!students.Any())
            {
                Console.WriteLine("No students enrolled in this course.");
            }
            else
            {
                foreach (var sc in students)
                {
                    string grade = sc.Grade.HasValue ? sc.Grade.Value.ToString() : "No Grade";
                    Console.WriteLine($"{sc.Student.FullName} → Grade: {grade}");
                }
            }

            Console.ReadKey();
        }

        // ---------------------------------------------------------
        // Input Helper Methods

        private string ReadCourseTitle()
        {
            while (true)
            {
                Console.Write("Course Title: ");
                string? title = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(title) && title.Length is >= 2 and <= 50)
                    return title;

                Console.WriteLine("Title must be between 2 and 50 characters.");
            }
        }

        private int ReadTeacherId()
        {
            Console.WriteLine("\nAvailable Teachers:");
            foreach (var t in _context.Teachers)
                Console.WriteLine($"{t.Id} - {t.FullName}");

            while (true)
            {
                Console.Write("Teacher Id: ");
                if (int.TryParse(Console.ReadLine(), out int id)
                    && _context.Teachers.Any(t => t.Id == id))
                    return id;

                Console.WriteLine("Invalid Teacher Id!");
            }
        }

        private int ReadCourseId()
        {
            if (!_context.Courses.Any())
            {
                Console.WriteLine("No courses found!");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("\nAvailable Courses:");
            foreach (var c in _context.Courses)
                Console.WriteLine($"{c.Id} - {c.Title}");

            while (true)
            {
                Console.Write("Course Id: ");
                if (int.TryParse(Console.ReadLine(), out int id)
                    && _context.Courses.Any(c => c.Id == id))
                    return id;

                Console.WriteLine("Invalid Course Id!");
            }
        }
    }
}
