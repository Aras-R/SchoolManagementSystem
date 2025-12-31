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

        // Shows the course management menu
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Course Management ===");
                Console.WriteLine("----------------------------");
                Console.WriteLine("1) Add Course");
                Console.WriteLine("2) Edit Course");
                Console.WriteLine("3) Delete Course");
                Console.WriteLine("4) List Courses");
                Console.WriteLine("----------------------------");
                Console.WriteLine("5) Add Student to Course");
                Console.WriteLine("6) Remove Student from Course");
                Console.WriteLine("7) Assign Grade");
                Console.WriteLine("8) List Course Students");
                Console.WriteLine("----------------------------");
                Console.WriteLine("0) Back");
                Console.WriteLine("----------------------------");
                Console.Write("Choose: ");

                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1": AddCourse(); break;
                    case "2": EditCourse(); break;
                    case "3": DeleteCourse(); break;
                    case "4": ListCourses(); break;
                    case "5": AddStudentToCourse(); break;
                    case "6": RemoveStudentFromCourse(); break;
                    case "7": AssignGrade(); break;
                    case "8": ListCourseStudents(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ---------------------------------------
        // Adds a new course with assigned teacher
        private void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Course ===");
            Console.WriteLine("------------------");

            if (!_context.Teachers.Any())
            {
                Console.WriteLine("No teachers found. Please add a teacher first.");
                Console.ReadKey();
                return;
            }

            string title = ReadCourseTitle();
            int teacherId = SelectTeacherId();

            var service = new AddCourseService(_context);
            var course = service.Add(title, teacherId);

            Console.WriteLine($"\nCourse created: {course.Title} (Teacher: {course.Teacher.FullName})");
            Console.ReadKey();
        }

        // ---------------------------------------
        // Edits an existing course
        private void EditCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Course ===");
            Console.WriteLine("-------------------");

            int courseId = SelectCourseId();
            if (courseId == -1) return;

            string newTitle = ReadCourseTitle();
            int newTeacherId = SelectTeacherId();

            var service = new EditCourseService(_context);
            service.Edit(courseId, newTitle, newTeacherId);

            Console.WriteLine("Course updated successfully!");
            Console.ReadKey();
        }

        // ---------------------------------------
        // Deletes a course
        private void DeleteCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Course ===");
            Console.WriteLine("---------------------");

            int courseId = SelectCourseId();
            if (courseId == -1) return;

            var service = new DeleteCourseService(_context);
            bool result = service.Delete(courseId);

            Console.WriteLine(result ? "Course deleted successfully!" : "Course not found!");
            Console.ReadKey();
        }

        // ---------------------------------------
        // Lists all courses with assigned teacher
        private void ListCourses()
        {
            Console.Clear();
            Console.WriteLine("=== Course List ===");
            Console.WriteLine("-------------------");

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
                    Console.WriteLine($"{course.Id} - {course.Title} (Teacher: {course.Teacher?.FullName ?? "No Teacher"})");
                }
            }

            Console.ReadKey();
        }

        // =======================================
        // Adds a student to a course
        private void AddStudentToCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Add Student to Course ===");
            Console.WriteLine("-----------------------------");

            int courseId = SelectCourseId();
            if (courseId == -1) return; // no courses available or invalid input

            int studentId = SelectStudentId();
            if (studentId == -1) return; // no students available or invalid input

            var service = new AddStudentToCourseService(_context);

            try
            {
                service.Add(studentId, courseId);
                Console.WriteLine("Student added to course successfully!");
            }
            catch (Exception ex)
            {
                // student not found, course not found, already registered
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey(); 
        }

        // ---------------------------------------
        // Removes a student from a course
        private void RemoveStudentFromCourse()
        {
            Console.Clear();
            Console.WriteLine("=== Remove Student from Course ===");
            Console.WriteLine("----------------------------------");

            int courseId = SelectCourseId();
            if (courseId == -1) return; // no courses available or invalid input

            // Select only students registered in the selected course
            int studentId = SelectStudentIdInCourse(courseId);
            if (studentId == -1) return; // no students registered in this course

            var service = new DeleteStudentFromCourseService(_context);

            bool result = service.Delete(studentId, courseId);

            if (result)
                Console.WriteLine("Student removed from course successfully!");
            else
                Console.WriteLine("Error: Student is not registered in this course.");

            Console.ReadKey();
        }

        // ---------------------------------------
        // Assigns a grade to a student in a course
        private void AssignGrade()
        {
            Console.Clear();
            Console.WriteLine("=== Assign Grade ===");
            Console.WriteLine("--------------------");

            int courseId = SelectCourseId();
            if (courseId == -1) return; // no courses available or invalid input

            // Select only students registered in the selected course
            int studentId = SelectStudentIdInCourse(courseId);
            if (studentId == -1) return; // no students registered in this course

            int grade = ReadGrade("Enter Grade (0-20): ");

            var service = new AssignGradeService(_context);

            try
            {
                service.Assign(studentId, courseId, grade);
                Console.WriteLine("Grade assigned successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
        }

        // ---------------------------------------
        // Lists all students enrolled in a course along with grades
        private void ListCourseStudents()
        {
            Console.Clear();
            Console.WriteLine("=== Course Students ===");
            Console.WriteLine("-----------------------");

            int courseId = SelectCourseId();
            if (courseId == -1) return; // no courses available or invalid input

            var service = new GetCourseStudentsService(_context);
            var students = service.Get(courseId);

            if (!students.Any())
            {
                Console.WriteLine("No students enrolled in this course.");
            }
            else
            {
                Console.WriteLine("Students enrolled in this course:");
                foreach (var sc in students)
                {
                    string grade = sc.Grade.HasValue ? sc.Grade.Value.ToString() : "No Grade";
                    Console.WriteLine($"{sc.Student.FullName} → Grade: {grade}");
                }
            }

            Console.ReadKey(); 
        }

        // ===========================================
        // Helper Methods

        // Displays courses and reads valid course ID
        private int SelectCourseId()
        {
            if (!_context.Courses.Any())
            {
                Console.WriteLine("No courses found!");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("Available Courses:");
            foreach (var c in _context.Courses)
                Console.WriteLine($"{c.Id} - {c.Title} (Teacher: {c.Teacher?.FullName ?? "No Teacher"})");

            while (true)
            {
                Console.Write("Course Id: ");
                if (int.TryParse(Console.ReadLine(), out int id) && _context.Courses.Any(c => c.Id == id))
                    return id;

                Console.WriteLine("Invalid Course Id!");
            }
        }

        // Displays students and reads valid student ID
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
                Console.WriteLine($"{s.Id} - {s.FullName}");

            while (true)
            {
                Console.Write("Student Id: ");
                if (int.TryParse(Console.ReadLine(), out int id) && _context.Students.Any(s => s.Id == id))
                    return id;

                Console.WriteLine("Invalid Student Id!");
            }
        }

        // Displays teachers and reads valid teacher ID
        private int SelectTeacherId()
        {
            if (!_context.Teachers.Any())
            {
                Console.WriteLine("No teachers found!");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("Available Teachers:");
            foreach (var t in _context.Teachers)
                Console.WriteLine($"{t.Id} - {t.FullName}");

            while (true)
            {
                Console.Write("Teacher Id: ");
                if (int.TryParse(Console.ReadLine(), out int id) && _context.Teachers.Any(t => t.Id == id))
                    return id;

                Console.WriteLine("Invalid Teacher Id!");
            }
        }

        // Reads a valid grade (0-20)
        private int ReadGrade(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int grade) && grade >= 0 && grade <= 20)
                    return grade;

                Console.WriteLine("Invalid grade! Must be between 0 and 20.");
            }
        }

        // Reads a valid course title
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

        // Helper method: select student registered in a specific course
        private int SelectStudentIdInCourse(int courseId)
        {
            var studentCourses = _context.StudentCourses
                .Where(sc => sc.CourseId == courseId)
                .ToList();

            if (!studentCourses.Any())
            {
                Console.WriteLine("No students registered in this course.");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("Registered Students:");
            foreach (var sc in studentCourses)
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == sc.StudentId);
                if (student != null)
                    Console.WriteLine($"{student.Id} - {student.FullName}");
            }

            while (true)
            {
                Console.Write("Student Id: ");
                if (int.TryParse(Console.ReadLine(), out int id) &&
                    studentCourses.Any(sc => sc.StudentId == id))
                    return id;

                Console.WriteLine("Invalid Student Id!");
                Console.ReadKey();
                return -1;
            }
        }

    }
}
