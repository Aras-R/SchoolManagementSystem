using SchoolManagementSystem.Data;
using SchoolManagementSystem.Services.Courses;
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
                Console.WriteLine("2) Add Student to Course");
                Console.WriteLine("3) Assign Grade to Student");
                Console.WriteLine("4) List Courses with Students and Grades");
                Console.WriteLine("5) Edit Course");
                Console.WriteLine("6) Delete Course");
                Console.WriteLine("0) Back");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": AddCourse(); break;
                    case "2": AddStudentToCourse(); break;
                    case "3": AssignGrade(); break;
                    case "4": ListCourses(); break;
                    case "5": EditCourse(); break;
                    case "6": DeleteCourse(); break;
                    case "0": return;

                    default:
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //-----------------------------------------------------------------------
        // Create a new lesson with the teacher
        private void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Course ===");

            if (_context.Teachers.Count == 0)
            {
                Console.WriteLine("No teachers found!");
                Console.WriteLine("Please add a teacher before creating a course.");
                Console.ReadKey();
                return;
            }

            string title = ReadCourseTitle();

            int teacherId = ReadTeacherId();
            if (teacherId == -1)   
                return;

            var service = new AddCourseService(_context);
            var course = service.Add(title, teacherId);

            Console.WriteLine($"\nCourse created: {course.Title} (Teacher: {course.Teacher.FullName})");
            Console.ReadKey();
        }


        // Adds a student to a course.
        private void AddStudentToCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Student to Course ===");

            int courseId = ReadCourseId();

            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                Console.ReadKey();
                return;
            }

            if (_context.Students.Count == 0)
            {
                Console.WriteLine("There are no students to add!");
                Console.ReadKey();
                return;
            }

            int studentId = ReadStudentId();

            if (course.Students.Any(s => s.Id == studentId))
            {
                Console.WriteLine("Student already assigned to this course!");
                Console.ReadKey();
                return;
            }

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

        // Assigns a grade to a student in a course.
        private void AssignGrade()
        {
            Console.Clear();
            Console.WriteLine("=== Assign Grade ===");

            int courseId = ReadCourseId();

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

            Console.WriteLine("\nStudents:");
            foreach (var s in course.Students)
                Console.WriteLine($"{s.Id} - {s.FullName}");

            int studentId = ReadStudentId();

            if (!course.Students.Any(s => s.Id == studentId))
            {
                Console.WriteLine("This student is not assigned to this course!");
                Console.ReadKey();
                return;
            }

            int grade = ReadGrade("Enter Grade (0–20): ");

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


        // Shows all courses with teacher name.
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

                foreach (var student in course.Students)
                {
                    string gradeDisplay = course.Grades.TryGetValue(student.Id, out int grade)
                        ? grade.ToString()
                        : "No Grade";

                    Console.WriteLine($"- {student.FullName} → Grade: {gradeDisplay}");
                }

                Console.WriteLine("-----------------------------");
            }

            Console.ReadKey();
        }

        // Edits a course’s title or teacher.
        private void EditCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Course ===");

            int courseId = ReadCourseId();

            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                Console.ReadKey();
                return;
            }

            string newTitle = ReadCourseTitle();

            int newTeacherId = ReadTeacherId();

            var service = new EditCourseService(_context);

            try
            {
                service.Edit(courseId, newTitle, newTeacherId);
                Console.WriteLine("\nCourse updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.ReadKey();
        }

        // Deletes a course.
        private void DeleteCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Course ===");

            int courseId = ReadCourseId();

            var service = new DeleteCourseService(_context);
            bool result = service.Delete(courseId);

            Console.WriteLine(result
                ? "\nCourse deleted successfully!"
                : "\nCourse not found!");

            Console.ReadKey();
        }


        // ---------------------------
        // Input Helper Methods

        // Reads a valid course title from the user.
        private string ReadCourseTitle()
        {
            while (true)
            {
                Console.Write("Course Title: ");
                string? title = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Title cannot be empty!");
                    continue;
                }

                if (title.Length < 2 || title.Length > 50)
                {
                    Console.WriteLine("Title must be 2–50 characters!");
                    continue;
                }

                return title;
            }
        }

        // Shows all teachers and reads a valid teacher ID.
        private int ReadTeacherId()
        {
            if (_context.Teachers.Count == 0)
            {
                Console.WriteLine("There are no teachers!");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("\nAvailable Teachers:");
            foreach (var t in _context.Teachers)
                Console.WriteLine($"{t.Id} - {t.FullName}");

            while (true)
            {
                Console.Write("Select Teacher Id: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int id) && _context.Teachers.Any(t => t.Id == id))
                    return id;

                Console.WriteLine("Invalid Teacher Id!");
            }
        }

        // Shows all courses and reads a valid course ID.
        private int ReadCourseId()
        {
            if (_context.Courses.Count == 0)
            {
                Console.WriteLine("There are no courses!");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("Available Courses:");
            foreach (var c in _context.Courses)
                Console.WriteLine($"{c.Id} - {c.Title}");

            while (true)
            {
                Console.Write("Enter Course Id: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int id) && _context.Courses.Any(c => c.Id == id))
                    return id;

                Console.WriteLine("Invalid Course Id!");
            }
        }

        // Shows all students and reads a valid student ID.
        private int ReadStudentId()
        {
            while (true)
            {
                Console.Write("Enter Student Id: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int id) && _context.Students.Any(s => s.Id == id))
                    return id;

                Console.WriteLine("Invalid Student Id!");
            }
        }

        // Reads a valid grade between 0 and 20.
        private int ReadGrade(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int grade))
                {
                    Console.WriteLine("Grade must be a number!");
                    continue;
                }

                if (grade < 0 || grade > 20)
                {
                    Console.WriteLine("Grade must be between 0 and 20!");
                    continue;
                }

                return grade;
            }
        }
    }
}
