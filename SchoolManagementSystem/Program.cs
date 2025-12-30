using SchoolManagementSystem.Data;
using SchoolManagementSystem.Menus;

internal class Program
{
    static void Main(string[] args)
    {
        // Shared in-memory data context
        DataContext context = new DataContext();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== School Management System ===");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1) Manage Students");
            Console.WriteLine("2) Manage Teachers");
            Console.WriteLine("3) Manage Courses");
            Console.WriteLine("4) Reports");              
            Console.WriteLine("--------------------------------");
            Console.WriteLine("0) Exit");
            Console.WriteLine("--------------------------------");
            Console.Write("Choose: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    new StudentMenu(context).Show();
                    break;

                case "2":
                    new TeacherMenu(context).Show();
                    break;

                case "3":
                    new CourseMenu(context).Show();
                    break;

                case "4":                                   
                    new ReportsMenu(context).Show();
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
}
