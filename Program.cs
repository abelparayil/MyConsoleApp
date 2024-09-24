using System;
using MyConsoleApp.Data;
using MyConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.IO;
using System.Globalization;

class Program
{
    private readonly DapperContext _dapperContext;

    public Program()
    {
        _dapperContext = new DapperContext();
    }
    public void Run()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Students");
            Console.WriteLine("2. Classes");
            Console.WriteLine("3. Professors");
            Console.WriteLine("0. Exit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ManageStudents();
                    break;
                case "2":
                    Console.WriteLine("Classes");
                    break;
                case "3":
                    ManageProfessors();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    private void ManageStudents()
    {
        bool exit = false;
        
        while(!exit) 
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. List Students");
            Console.WriteLine("2. Add Student");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("0. Exit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ListStudents();
                    break;
                case "2":
                    AddStudent();
                    break;
                case "3":
                    UpdateStudent();
                    break;
                case "4":
                    DeleteStudent();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    private void ManageClasses()
    {
        bool exit = false;
        
        while(!exit) 
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. List Classes");
            Console.WriteLine("2. Add Class");
            Console.WriteLine("3. Update Class");
            Console.WriteLine("4. Delete Class");
            Console.WriteLine("0. Exit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ListClasses();
                    break;
                case "2":
                    AddClass();
                    break;
                case "3":
                    UpdateClass();
                    break;
                case "4":
                    DeleteClass();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    private void ManageProfessors()
    {
        bool exit = false;
        
        while(!exit) 
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. List Professors");
            Console.WriteLine("2. Add Professor");
            Console.WriteLine("3. Update Professor");
            Console.WriteLine("4. Delete Professor");
            Console.WriteLine("0. Exit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ListProfessors();
                    break;
                case "2":
                    AddProfessor();
                    break;
                case "3":
                    UpdateProfessor();
                    break;
                case "4":
                    DeleteProfessor();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    // Student methods

    private void ListStudents()
    {
        string sql = "SELECT * FROM Students";
        IEnumerable<Student> students = _dapperContext.LoadData<Student>(sql);

        Console.WriteLine("'StudentId', 'FirstName', 'LastName', 'DOB', 'Email', 'PhoneNumber'");

        foreach (Student student in students)
        {
            Console.WriteLine("'" + student.StudentId + "', '" + student.FirstName + "', '" + student.LastName + "', '" + student.DOB + "', '" + student.Email + "', '" + student.PhoneNumber + "'");
        }
    }

    private void AddStudent()
    {
        Console.WriteLine("Enter student first name:");
        string firstName = Console.ReadLine();

        Console.WriteLine("Enter student last name:");
        string lastName = Console.ReadLine();

        Console.WriteLine("Enter student date of birth:");
        string dobInput = Console.ReadLine();
        DateTime dob;
        if (!DateTime.TryParseExact(dobInput,"MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
        {
            Console.WriteLine("Invalid date of birth");
            return;
        }

        Console.WriteLine("Enter student email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter student phone number:");
        string phoneNumber = Console.ReadLine();

        string sql = "INSERT INTO Students (FirstName, LastName, DOB, Email, PhoneNumber) VALUES (@FirstName, @LastName, @DOB, @Email, @PhoneNumber)";
        var parameters = new { FirstName = firstName, LastName = lastName, DOB = dob, Email = email, PhoneNumber = phoneNumber };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Student added successfully");
        }
        else
        {
            Console.WriteLine("Error adding student");
        }
    }

    private void UpdateStudent()
    {
        Console.WriteLine("Enter student id:");
        int studentId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter student first name:");
        string firstName = Console.ReadLine();

        Console.WriteLine("Enter student last name:");
        string lastName = Console.ReadLine();

        Console.WriteLine("Enter student date of birth:");
        string dobInput = Console.ReadLine();
        DateTime dob;
        if (!DateTime.TryParseExact(dobInput,"MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
        {
            Console.WriteLine("Invalid date of birth");
            return;
        }

        Console.WriteLine("Enter student email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter student phone number:");
        string phoneNumber = Console.ReadLine();

        string sql = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, DOB = @DOB, Email = @Email, PhoneNumber = @PhoneNumber WHERE StudentId = @StudentId";
        var parameters = new { StudentId = studentId, FirstName = firstName, LastName = lastName, DOB = dob, Email = email, PhoneNumber = phoneNumber };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Student updated successfully");
        }
        else
        {
            Console.WriteLine("Error updating student");
        }
    }

    private void DeleteStudent()
    {
        Console.WriteLine("Enter student id:");
        int studentId = int.Parse(Console.ReadLine());

        string sql = "DELETE FROM Students WHERE StudentId = @StudentId";
        var parameters = new { StudentId = studentId };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Student deleted successfully");
        }
        else
        {
            Console.WriteLine("Error deleting student");
        }
    }

    private void ListProfessors()
    {
        string sql = "SELECT * FROM Professors";
        IEnumerable<Professor> professors = _dapperContext.LoadData<Professor>(sql);

        Console.WriteLine("'ProfessorId', 'FirstName', 'LastName', 'Email', 'PhoneNumber'");

        foreach (Professor professor in professors)
        {
            Console.WriteLine("'" + professor.ProfessorId + "', '" + professor.FirstName + "', '" + professor.LastName + "', '" + professor.Email + "', '" + professor.PhoneNumber + "'");
        }
    }

    private void AddProfessor()
    {
        Console.WriteLine("Enter professor first name:");
        string firstName = Console.ReadLine();

        Console.WriteLine("Enter professor last name:");
        string lastName = Console.ReadLine();

        Console.WriteLine("Enter professor email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter professor phone number:");
        string phoneNumber = Console.ReadLine();

        string sql = "INSERT INTO Professors (FirstName, LastName, Email, PhoneNumber) VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";
        var parameters = new { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Professor added successfully");
        }
        else
        {
            Console.WriteLine("Error adding professor");
        }
    }

    private void UpdateProfessor()
    {
        Console.WriteLine("Enter professor id:");
        int professorId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter professor first name:");
        string firstName = Console.ReadLine();

        Console.WriteLine("Enter professor last name:");
        string lastName = Console.ReadLine();

        Console.WriteLine("Enter professor email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter professor phone number:");
        string phoneNumber = Console.ReadLine();

        string sql = "UPDATE Professors SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber WHERE ProfessorId = @ProfessorId";
        var parameters = new { ProfessorId = professorId, FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Professor updated successfully");
        }
        else
        {
            Console.WriteLine("Error updating professor");
        }
    }

    private void DeleteProfessor()
    {
        Console.WriteLine("Enter professor id:");
        int professorId = int.Parse(Console.ReadLine());

        string sql = "DELETE FROM Professors WHERE ProfessorId = @ProfessorId";
        var parameters = new { ProfessorId = professorId };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Professor deleted successfully");
        }
        else
        {
            Console.WriteLine("Error deleting professor");
        }
    }

    private void ListClasses()
    {
        string sql = "SELECT * FROM Classes";

        IEnumerable<Class> classes = _dapperContext.LoadData<Class>(sql);

        Console.WriteLine("'ClassId', 'ClassName', 'ClassCode', 'Credits'");
        foreach (Class classObj in classes)
        {
            Console.WriteLine("'" + classObj.ClassId + "', '" + classObj.ClassName + "', '" + classObj.ClassCode + "', '" + classObj.Credits + "'");
        }
    }

    private void AddClass()
    {
        Console.WriteLine("Enter class name:");
        string className = Console.ReadLine();

        Console.WriteLine("Enter class code:");
        string classCode = Console.ReadLine();

        Console.WriteLine("Enter class credits:");
        int credits = int.Parse(Console.ReadLine());

        string sql = "INSERT INTO Classes (ClassName, ClassCode, Credits) VALUES (@ClassName, @ClassCode, @Credits)";
        var parameters = new { ClassName = className, ClassCode = classCode, Credits = credits };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Class added successfully");
        }
        else
        {
            Console.WriteLine("Error adding class");
        }
    }

    private void UpdateClass()
    {
        Console.WriteLine("Enter class id:");
        int classId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter class name:");
        string className = Console.ReadLine();

        Console.WriteLine("Enter class code:");
        string classCode = Console.ReadLine();

        Console.WriteLine("Enter class credits:");
        int credits = int.Parse(Console.ReadLine());

        string sql = "UPDATE Classes SET ClassName = @ClassName, ClassCode = @ClassCode, Credits = @Credits WHERE ClassId = @ClassId";
        var parameters = new { ClassId = classId, ClassName = className, ClassCode = classCode, Credits = credits };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Class updated successfully");
        }
        else
        {
            Console.WriteLine("Error updating class");
        }
    }

    private void DeleteClass()
    {
        Console.WriteLine("Enter class id:");
        int classId = int.Parse(Console.ReadLine());

        string sql = "DELETE FROM Classes WHERE ClassId = @ClassId";
        var parameters = new { ClassId = classId };

        bool success = _dapperContext.ExecuteSql(sql, parameters);

        if (success)
        {
            Console.WriteLine("Class deleted successfully");
        }
        else
        {
            Console.WriteLine("Error deleting class");
        }
    }

    static void Main(string[] args)
    {
        Program program = new Program();
        program.Run();
    }
}
