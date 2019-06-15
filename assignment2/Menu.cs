using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment2
{
    class Menu
    {
        #region print navigation options
        private readonly string options =
               "\n****************************************************************************************" +
               "\n 1: list all students                          | 10: add new student " +
               "\n 2: list all trainers                          | 11: add new trainer  " +
               "\n 3: list all assignments                       | 12: add new assignment  " +
               "\n 4: list all courses                           | 13: add new course  " +
               "\n 5: list students per course                   | 14: add student to course " +
               "\n 6: list trainers per course                   | 15: add trainer to course " +
               "\n 7: list assignments per course                | 16: add assignment to course" +
               "\n 8: list assignments per student per course    | 17: exit " +
               "\n 9: list students in more than one course      | " +
               "\n****************************************************************************************" +
               "\n Please choose an action from the menu (1-17):"; 
        #endregion
        #region  methods
        private static T New<T>() where T : new()
        {
            T t = new T();
            foreach (var prop in typeof(T).GetProperties().Skip(1))
            {
                Console.Write($" Give {typeof(T).Name}'s {prop.Name}:");
                if (prop.PropertyType.Name == "DateTime")
                {
                    DateTime value;
                    while (!DateTime.TryParse(Console.ReadLine(), out value))
                    {
                        Console.Write(" Wrong Input. Enter a valid date (d/m/y):");
                    }
                    prop.SetValue(t, value);
                }
                else if (prop.PropertyType.Name == "Decimal")
                {
                    decimal value;
                    while (!Decimal.TryParse(Console.ReadLine(), out value))
                    {
                        Console.Write(" Wrong Input. Enter a number:");
                    }
                    prop.SetValue(t, value);
                }
                else
                {
                    var value = Convert.ChangeType(Console.ReadLine(), prop.PropertyType);
                    prop.SetValue(t, value);
                }
            }
            return t;
        }
        private static XInCourse NewToCourse(string name)
        {
            XInCourse x = new XInCourse();
            int value;
            x.Name = name;
            Console.Write($" Give {x.Name}'s Id:");
            while (!Int32.TryParse(Console.ReadLine(), out value))
            {
                Console.Write(" Wrong input. Please give an integer:");
            }
            x.Id = value;
            Console.Write(" Give Course's Id:");
            while (!Int32.TryParse(Console.ReadLine(), out value))
            {
                Console.Write(" Wrong input. Please give an integer:");
            }
            x.CourseId = value;
            return x;
        } 
        #endregion
        #region execute navigation options
        public Menu(DbManager db)
        {
            Console.WriteLine(" Welcome to our school!");
            bool exit = false;
            while (!exit)
            {
                Console.Write(options);
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine(db.ListOf<Student>());
                        break;
                    case "2":
                        Console.WriteLine(db.ListOf<Trainer>());
                        break;
                    case "3":
                        Console.WriteLine(db.ListOf<Assignment>());
                        break;
                    case "4":
                        Console.WriteLine(db.ListOf<Course>());
                        break;
                    case "5":
                        Console.WriteLine(db.ListOf<StudentPerCourse>());
                        break;
                    case "6":
                        Console.WriteLine(db.ListOf<TrainerPerCourse>());
                        break;
                    case "7":
                        Console.WriteLine(db.ListOf<AssignmentPerCourse>());
                        break;
                    case "8":
                        Console.WriteLine(db.ListOf<AssignmentPerCoursePerStudent>());
                        break;
                    case "9":
                        Console.WriteLine(db.ListOf<StudentInMoreCourses>());
                        break;
                    case "10":
                        db.Add(New<Student>());
                        break;
                    case "11":
                        db.Add(New<Trainer>());
                        break;
                    case "12":
                        db.Add(New<Assignment>());
                        break;
                    case "13":
                        db.Add(New<Course>());
                        break;
                    case "14":
                        db.AddToCourse(NewToCourse("Student"));
                        break;
                    case "15":
                        db.AddToCourse(NewToCourse("Trainer"));
                        break;
                    case "16":
                        db.AddToCourse(NewToCourse("Assignment"));
                        break;
                    case "17":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong input.");
                        break;
                }
                Console.WriteLine(db.SuccessMesssage);
            }
        } 
        #endregion
    }
}
