using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Menus
{
    public class StudentMenu
    {
        private readonly DataContext _context;

        public StudentMenu(DataContext context)
        {
            _context = context;
        }

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
                Console.WriteLine("0) Back");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddStudent();
                        break;

                    case "2":
                        EditStudent();
                        break;

                    case "3":
                        DeleteStudent();
                        break;

                    case "4":
                        ListStudents();
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

        private void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Add Student ===");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine()!;

            Console.Write("Student Number: ");
            string studentNumber = Console.ReadLine()!;

            Console.Write("Birth Date (yyyy-mm-dd): ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine()!);

            Console.Write("Major: ");
            string major = Console.ReadLine()!;

            var service = new AddStudentService(_context);
            var student = service.Add(firstName, lastName, studentNumber, birthDate, major);

            Console.WriteLine($"\nStudent Added: {student.FullName}");
            Console.ReadKey();
        }

        private void EditStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Student ===");

            Console.Write("Enter Student Id: ");
            int id = int.Parse(Console.ReadLine()!);

            var getService = new GetStudentByIdService(_context);
            var student = getService.Get(id);

            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Editing: {student.FullName}");

            Console.Write("New First Name: ");
            string firstName = Console.ReadLine()!;

            Console.Write("New Last Name: ");
            string lastName = Console.ReadLine()!;

            Console.Write("New Student Number: ");
            string studentNumber = Console.ReadLine()!;

            Console.Write("New Birth Date (yyyy-mm-dd): ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine()!);

            Console.Write("New Major: ");
            string major = Console.ReadLine()!;

            var editService = new EditStudentService(_context);
            editService.Edit(id, firstName, lastName, studentNumber, birthDate, major);

            Console.WriteLine("\nStudent updated successfully!");
            Console.ReadKey();
        }

        private void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Student ===");

            Console.Write("Enter Student Id: ");
            int id = int.Parse(Console.ReadLine()!);

            var deleteService = new DeleteStudentService(_context);
            bool result = deleteService.Delete(id);

            if (!result)
                Console.WriteLine("Student not found!");
            else
                Console.WriteLine("Student deleted successfully!");

            Console.ReadKey();
        }

        private void ListStudents()
        {
            Console.Clear();
            Console.WriteLine("=== Student List ===");

            var listService = new ListStudentsService(_context);
            var students = listService.GetAll();

            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }
            }

            Console.ReadKey();
        }
    }
}
