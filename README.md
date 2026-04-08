# VgcCollege.Web

## Project Overview
VgcCollege.Web is an ASP.NET Core MVC college management system developed for academic purposes.  
The system manages branches, courses, student enrolments, attendance, assignments, assignment results, exams, and exam results.

It also includes authentication and authorization with different user roles:
- Admin
- Faculty
- Student

---

## Features
- User authentication with ASP.NET Core Identity
- Role-based authorization
- Branch management
- Course management
- Course enrolment management
- Attendance tracking
- Assignment management
- Assignment result management
- Exam management
- Exam result management
- Student portal
- Faculty portal
- Released exam result filtering for students
- xUnit unit tests
- GitHub Actions CI

---

## Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- ASP.NET Core Identity
- xUnit
- GitHub Actions

---

## Test Users

### Admin
- **Email:** admin@vgc.ie
- **Password:** Admin123!

### Faculty
- **Email:** faculty@vgc.ie
- **Password:** Faculty123!

### Student 1
- **Email:** student1@vgc.ie
- **Password:** Student123!

### Student 2
- **Email:** student2@vgc.ie
- **Password:** Student123!

---

## How to Run the Project
1. Open the solution in JetBrains Rider or Visual Studio.
2. Restore NuGet packages.
3. Apply migrations if needed.
4. Run the project.
5. Open the local browser URL shown by ASP.NET Core.

---

## How to Run Tests
1. Open the test project `VgcCollege.Tests`.
2. Run all tests using Rider, Visual Studio, or the command line.
3. Example command:

```bash
dotnet test
