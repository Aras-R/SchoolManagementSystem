using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services.Teachers;
using System;
using System.Linq;

namespace SchoolManagementSystem.Menus
{
    public class TeacherMenu
    {
        private readonly DataContext _context;

        public TeacherMenu(DataContext context)
        {
            _context = context;
        }

        // Shows the teacher management menu.
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Teacher Management ===");
                Console.WriteLine("1) Add Teacher");
                Console.WriteLine("2) Edit Teacher");
                Console.WriteLine("3) Delete Teacher");
                Console.WriteLine("4) List Teachers");
                Console.WriteLine("0) Back");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddTeacher();
                        break;

                    case "2":
                        EditTeacher();
                        break;

                    case "3":
                        DeleteTeacher();
                        break;

                    case "4":
                        ListTeachers();
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

        // ----------------------------------------------------------------
        // Creates a new teacher.
        private void AddTeacher()
        {
            Console.Clear();
            Console.WriteLine("=== Add Teacher ===");

            string firstName = ReadName("First Name: ");
            string lastName = ReadName("Last Name: ");
            string teacherCode = ReadTeacherCode("Teacher Code (11 digits): ");
            string college = ReadRequired("College: ");

            var service = new AddTeacherService(_context);
            var teacher = service.Add(teacherCode, firstName, lastName, college);

            Console.WriteLine($"\nTeacher Added: {teacher.FullName}");
            Console.ReadKey();
        }

        // Edits an existing teacher.
        private void EditTeacher()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Teacher ===");

            Console.Write("Enter Teacher Id: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id!");
                Console.ReadKey();
                return;
            }

            var getService = new GetTeacherByIdService(_context);
            var teacher = getService.GetById(id);

            if (teacher == null)
            {
                Console.WriteLine("Teacher not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Editing: {teacher.FullName}");

            string firstName = ReadName("New First Name: ");
            string lastName = ReadName("New Last Name: ");
            string teacherCode = ReadTeacherCode("New Teacher Code (11 digits): ");
            string college = ReadRequired("New College: ");

            var editService = new EditTeacherService(_context);
            editService.Edit(id, teacherCode, firstName, lastName, college);

            Console.WriteLine("\nTeacher updated successfully!");
            Console.ReadKey();
        }

        // Deletes a teacher.
        private void DeleteTeacher()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Teacher ===");

            Console.Write("Enter Teacher Id: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id!");
                Console.ReadKey();
                return;
            }

            var deleteService = new DeleteTeacherService(_context);
            bool result = deleteService.Delete(id);

            Console.WriteLine(result ? "Teacher deleted successfully!" : "Teacher not found!");
            Console.ReadKey();
        }

        // Shows all teachers.
        private void ListTeachers()
        {
            Console.Clear();
            Console.WriteLine("=== Teacher List ===");

            var listService = new ListTeachersService(_context);
            var teachers = listService.GetAll();

            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found.");
            }
            else
            {
                foreach (var teacher in teachers)
                {
                    Console.WriteLine(teacher.FullInfo());
                    Console.WriteLine("-----------------------------");
                }
            }

            Console.ReadKey();
        }


        // ---------------------------------------------------------------
        // Input Helper Methods

        // Reads a required string.
        private string ReadRequired(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("This field cannot be empty!");
            }
        }

        // Reads a validated name with length limits
                private string ReadName(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                string? name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty!");
                    continue;
                }

                if (name.Length < 2 || name.Length > 50)
                {
                    Console.WriteLine("Name must be between 2 and 50 characters!");
                    continue;
                }

                return name.Trim();
            }
        }

        // Reads an 11-digit numeric TeacherCode
        private string ReadTeacherCode(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Teacher code cannot be empty!");
                    continue;
                }

                if (input.Length != 11 || !input.All(char.IsDigit))
                {
                    Console.WriteLine("Teacher code must be 11 digits and numeric!");
                    continue;
                }

                if (_context.Teachers.Any(t => t.TeacherCode == input))
                {
                    Console.WriteLine("This teacher code already exists!");
                    continue;
                }

                return input;
            }
        }
    }
}
