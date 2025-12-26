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

        // Shows the student management menu.
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Student Management ===");
                Console.WriteLine("1) Add Student");
                Console.WriteLine("2) Edit Student");
                Console.WriteLine("3) Delete Student");
                Console.WriteLine("4) List Students");
                Console.WriteLine("4) --------------------------------");
                Console.WriteLine("5) Add Student To Course");
                Console.WriteLine("6) View Student Courses");
                Console.WriteLine("7) Assign Grade");
                Console.WriteLine("8) Remove Student From Course");
                Console.WriteLine("0) Back");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": AddStudent(); break;
                    case "2": EditStudent(); break;
                    case "3": DeleteStudent(); break;
                    case "4": ListStudents(); break;
                    case "5": AddStudentToCourse(); break;
                    case "6": ListStudentCourses(); break;
                    case "7": AssignGrade(); break;
                    case "8": RemoveStudentFromCourse(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ---------------------------------------------------------
        // Student CRUD

        private void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Add Student ===");

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

        private void EditStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Student ===");

            int id = ReadId("Enter Student Id: ");

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

        private void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Student ===");

            int id = ReadId("Enter Student Id: ");

            var deleteService = new DeleteStudentService(_context);
            bool result = deleteService.Delete(id);

            Console.WriteLine(result ? "Student deleted successfully!" : "Student not found!");
            Console.ReadKey();
        }

        private void ListStudents()
        {
            Console.Clear();
            Console.WriteLine("=== Student List ===");

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

        // ---------------------------------------------------------
        // StudentCourse Operations

        private void AddStudentToCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Student To Course ===");

            int studentId = ReadId("Student Id: ");
            int courseId = ReadId("Course Id: ");

            var service = new AddStudentToCourseService(_context);
            service.Add(studentId, courseId);

            Console.WriteLine("Student added to course successfully!");
            Console.ReadKey();
        }

        private void ListStudentCourses()
        {
            Console.Clear();
            Console.WriteLine("=== Student Courses ===");

            int studentId = ReadId("Student Id: ");

            var service = new GetStudentCoursesService(_context);
            var courses = service.Get(studentId);

            if (!courses.Any())
            {
                Console.WriteLine("No courses found for this student.");
            }
            else
            {
                foreach (var course in courses)
                {
                    Console.WriteLine(course);
                }
            }

            Console.ReadKey();
        }

        private void AssignGrade()
        {
            Console.Clear();
            Console.WriteLine("=== Assign Grade ===");

            int studentId = ReadId("Student Id: ");
            int courseId = ReadId("Course Id: ");
            int grade = ReadId("Grade (0-20): ");

            var service = new AssignGradeService(_context);
            service.Assign(studentId, courseId, grade);

            Console.WriteLine("Grade assigned successfully!");
            Console.ReadKey();
        }

        private void RemoveStudentFromCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Remove Student From Course ===");

            int studentId = ReadId("Student Id: ");
            int courseId = ReadId("Course Id: ");

            var service = new DeleteStudentFromCourseService(_context);
            service.Delete(studentId, courseId);

            Console.WriteLine("Student removed from course successfully!");
            Console.ReadKey();
        }

        // ---------------------------------------------------------
        // Input Helpers (UNCHANGED STYLE)

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
