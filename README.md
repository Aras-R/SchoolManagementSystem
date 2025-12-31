# School Management System
A simple console application to manage students, teachers, courses, enrollments, and grades.

## Features
- **Students Management**
  - Add, edit, delete, and list students
  - Getting a student's course list
- **Teachers Management**
  - Add, edit, delete, and list teachers
- **Courses Management**
  - Add, edit, delete, and list courses
  - Assign teachers to courses
  - Enroll students in courses
  - Remove students from courses
  - Assign grades to students
  - List students enrolled in a course with grades
- **Reports**
  - Calculate average grade for a student
  - Calculate average grade for a course
  - Calculate overall average grade for all students

## How to Run
1. Clone the repository
2. Open the solution in Visual Studio
3. Build and run the project
4. Use the console menus to manage students, teachers, courses, and view reports

## Project Structure
- **Models:** Contains `Student`, `Teacher`, `Course`, and `StudentCourse` classes
- **Services:** Business logic organized by entity and functionality:
  - **Students:** AddStudentService, EditStudentService, DeleteStudentService, GetStudentByIdService, ListStudentsService
  - **Teachers:** AddTeacherService, EditTeacherService, DeleteTeacherService, GetTeacherByIdService, ListTeachersService
  - **Courses:** AddCourseService, EditCourseService, DeleteCourseService, ListCoursesService
  - **StudentCourses:** AddStudentToCourseService, DeleteStudentFromCourseService, AssignGradeService, GetCourseStudentsService, GetStudentCoursesService
  - **Calculations:** CalculateStudentAverageService, CalculateCourseAverageService, CalculateOverallAverageService
- **Menus:** Console menus for interacting with the system:
  - CourseMenu.cs
  - StudentMenu.cs
  - TeacherMenu.cs
  - ReportsMenu.cs
- **Data:** In-memory database (DataContext.cs)
- **Program.cs:** Application entry point

## Requirements
- .NET 9.0 or later
- Visual Studio or any C# IDE

## Notes
- The system uses an **in-memory database**, so all data will be lost when the application is closed.
- Input validation is included for IDs, grades, and titles.
- Error handling provides user-friendly messages and returns to the menu without crashing.
