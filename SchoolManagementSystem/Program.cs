using SchoolManagementSystem.Data;
using SchoolManagementSystem.Menus;

internal class Program
{
    static void Main(string[] args)
    {
        DataContext context = new DataContext();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== School Management System ===");
            Console.WriteLine("1) Manage Students");
            Console.WriteLine("2) Manage Teachers");
            Console.WriteLine("3) Manage Courses");
            Console.WriteLine("0) Exit");
            Console.Write("Choose: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var studentMenu = new StudentMenu(context);
                    studentMenu.Show();
                    break;

                case "2":
                    var teacherMenu = new TeacherMenu(context);
                    teacherMenu.Show();
                    break;

                case "3":
                    Console.WriteLine("Course management will be added soon.");
                    Console.ReadKey();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid input.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
