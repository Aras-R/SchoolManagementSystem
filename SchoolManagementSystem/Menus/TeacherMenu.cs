using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Menus
{
    public class TeacherMenu
    {
        private readonly DataContext _context;

        public TeacherMenu(DataContext context)
        {
            _context = context;
        }

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

        private void AddTeacher()
        {
            Console.Clear();
            Console.WriteLine("=== Add Teacher ===");

            string teacherCode = ReadNumeric("Teacher Code: ");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine()!;

            Console.Write("College: ");
            string college = Console.ReadLine()!;

            var service = new AddTeacherService(_context);
            var teacher = service.Add(teacherCode, firstName, lastName, college);

            Console.WriteLine($"\nTeacher Added: {teacher.FullName}");
            Console.ReadKey();
        }

        private void EditTeacher()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Teacher ===");

            Console.Write("Enter Teacher Id: ");
            int id = int.Parse(Console.ReadLine()!);

            var getService = new GetTeacherByIdService(_context);
            var teacher = getService.GetById(id);

            if (teacher == null)
            {
                Console.WriteLine("Teacher not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Editing: {teacher.FullName}");

            string teacherCode = ReadNumeric("New Teacher Code: ");

            Console.Write("New First Name: ");
            string firstName = Console.ReadLine()!;

            Console.Write("New Last Name: ");
            string lastName = Console.ReadLine()!;

            Console.Write("New College: ");
            string college = Console.ReadLine()!;

            var editService = new EditTeacherService(_context);
            editService.Edit(id, teacherCode, firstName, lastName, college);

            Console.WriteLine("\nTeacher updated successfully!");
            Console.ReadKey();
        }

        private void DeleteTeacher()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Teacher ===");

            Console.Write("Enter Teacher Id: ");
            int id = int.Parse(Console.ReadLine()!);

            var deleteService = new DeleteTeacherService(_context);
            bool result = deleteService.Delete(id);

            if (!result)
                Console.WriteLine("Teacher not found!");
            else
                Console.WriteLine("Teacher deleted successfully!");

            Console.ReadKey();
        }

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

        // Only numeric inputs allowed
        private string ReadNumeric(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && input.All(char.IsDigit))
                    return input;

                Console.WriteLine("❌ Only numbers are allowed!");
            }
        }
    }
}

