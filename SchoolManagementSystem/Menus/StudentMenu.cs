using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services.StudentCourses;
using SchoolManagementSystem.Services.Students;
using System;
using System.Linq;

namespace SchoolManagementSystem.Menus
{
    public class StudentMenu
    {
        private readonly DataContext _context;

        public StudentMenu(DataContext context)
        {
            _context = context;
        }

        // Shows the student management menu
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Student Management ===");
                Console.WriteLine("----------------------------");
                Console.WriteLine("1) Add Student");
                Console.WriteLine("2) Edit Student");
                Console.WriteLine("3) Delete Student");
                Console.WriteLine("4) List Students");
                Console.WriteLine("5) View Student Courses");
                Console.WriteLine("----------------------------");
                Console.WriteLine("0) Back");
                Console.WriteLine("----------------------------");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1": AddStudent(); break;
                    case "2": EditStudent(); break;
                    case "3": DeleteStudent(); break;
                    case "4": ListStudents(); break;
                    case "5": ListStudentCourses(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // -----------------------
        // Adds a new student
        private void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Add Student ===");
            Console.WriteLine("-------------------");

            string firstName = ReadName("First Name");
            string lastName = ReadName("Last Name");
            string studentNumber = ReadStudentNumber();
            DateTime birthDate = ReadDate("Birth Date (yyyy-mm-dd): ");
            string major = ReadRequired("Major");

            var service = new AddStudentService(_context);
            var student = service.Add(firstName, lastName, studentNumber, birthDate, major);

            Console.WriteLine($"\nStudent Added: {student.FullName}");
            Console.ReadKey();
        }

        // -----------------------
        // Edits an existing student
        private void EditStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Student ===");
            Console.WriteLine("--------------------");

            int id = SelectStudentId();
            if (id == -1) return;

            var getService = new GetStudentByIdService(_context);
            var student = getService.Get(id);

            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Editing: {student.FullName}");
            string firstName = ReadName("New First Name");
            string lastName = ReadName("New Last Name");
            string studentNumber = ReadStudentNumber(id);
            DateTime birthDate = ReadDate("New Birth Date (yyyy-mm-dd): ");
            string major = ReadRequired("New Major");

            var editService = new EditStudentService(_context);
            editService.Edit(id, firstName, lastName, studentNumber, birthDate, major);

            Console.WriteLine("\nStudent updated successfully!");
            Console.ReadKey();
        }

        // -----------------------
        // Deletes a student
        private void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Student ===");
            Console.WriteLine("----------------------");

            int id = SelectStudentId();
            if (id == -1) return;

            var deleteService = new DeleteStudentService(_context);
            bool result = deleteService.Delete(id);

            Console.WriteLine(result ? "Student deleted successfully!" : "Student not found!");
            Console.ReadKey();
        }

        // -----------------------
        // Lists all students with details
        private void ListStudents()
        {
            Console.Clear();
            Console.WriteLine("=== Student List ===");
            Console.WriteLine("--------------------");

            var listService = new ListStudentsService(_context);
            var students = listService.GetAll();

            if (!students.Any())
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                foreach (var student in students)
                {
                    Console.WriteLine(student.FullInfo());
                    Console.WriteLine("-----------------------------");
                }
            }

            Console.ReadKey();
        }

        // -----------------------
        // Shows all courses a student is enrolled in
        private void ListStudentCourses()
        {
            Console.Clear();
            Console.WriteLine("=== Student Courses ===");

            int studentId = SelectStudentId();
            if (studentId == -1) return;

            var service = new GetStudentCoursesService(_context);
            var courses = service.Get(studentId);

            if (!courses.Any())
            {
                Console.WriteLine("No courses found for this student.");
            }
            else
            {
                Console.WriteLine("Courses enrolled:");
                foreach (var item in courses)
                {
                    string gradeDisplay = item.Grade.HasValue ? item.Grade.Value.ToString() : "No Grade";
                    Console.WriteLine($"{item.Course.Id} - {item.Course.Title} (Teacher: {item.Course.Teacher?.FullName ?? "No Teacher"}) → Grade: {gradeDisplay}");
                }
            }

            Console.ReadKey();
        }

        // -----------------------
        // Helper: Displays students and reads a valid ID
        private int SelectStudentId()
        {
            if (!_context.Students.Any())
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("Available Students:");
            foreach (var s in _context.Students)
            {
                Console.WriteLine($"{s.Id} - {s.FullName}");
            }

            return ReadId("Enter Student Id: ");
        }

        // -----------------------
        // Input Helpers

        private int ReadId(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int id))
                    return id;

                Console.WriteLine("Invalid number!");
            }
        }

        private string ReadName(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.Length is >= 2 and <= 50)
                    return input;

                Console.WriteLine("Name must be between 2 and 50 characters.");
            }
        }

        private string ReadStudentNumber(int? editingId = null)
        {
            while (true)
            {
                Console.Write("Student Number (11 digits): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || !input.All(char.IsDigit))
                {
                    Console.WriteLine("Student number must be numeric.");
                    continue;
                }

                if (input.Length != 11)
                {
                    Console.WriteLine("Student number must be exactly 11 digits.");
                    continue;
                }

                bool exists = _context.Students.Any(s => s.StudentNumber == input && s.Id != editingId);
                if (exists)
                {
                    Console.WriteLine("Student number already exists!");
                    continue;
                }

                return input;
            }
        }

        private DateTime ReadDate(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    return date;

                Console.WriteLine("Invalid date format!");
            }
        }

        private string ReadRequired(string message)
        {
            while (true)
            {
                Console.Write($"{message}: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("This field is required!");
            }
        }
    }
}
