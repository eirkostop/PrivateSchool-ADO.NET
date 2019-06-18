using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment2
{
    class Student
    {
        public int StudentId { private get; set; }
        public string FirstName { private get; set; }
        public string LastName { private get; set; }
        public DateTime DateOfBirth { private get; set; }
        public decimal TuitionFees { private get; set; }
    }
    class Trainer
    {
        public int TrainerId { private get; set; }
        public string FirstName { private get; set; }
        public string LastName { private get; set; }
        public string subject { private get; set; }
    }
    class Assignment
    {
        public int AssignmentId { private get; set; }
        public string Title { private get; set; }
        public string Description { private get; set; }
        public DateTime SubmissionDate { private get; set; }
    }
    class Course
    {
        public int CourseId { private get; set; }
        public string Title { private get; set; }
        public string Stream { private get; set; }
        public string Type { private get; set; }
        public DateTime StartDate { private get; set; }
        public DateTime EndDate { private get; set; }
    }
    class StudentPerCourse
    {
        public int CourseId { private get; set; }
        public string Course { private get; set; }
        public int StudentId { private get; set; }
        public string Name { private get; set; }
    }
    class TrainerPerCourse
    {
        public int CourseId { private get; set; }
        public string Course { private get; set; }
        public int TrainerId { private get; set; }
        public string Name { private get; set; }
    }
    class AssignmentPerCourse
    {
        public int CourseId { private get; set; }
        public string Course { private get; set; }
        public int AssignmentId { private get; set; }
        public string Assignment { private get; set; }
    }
    class AssignmentPerCoursePerStudent
    {
        public int StudentId { private get; set; }
        public string Name { private get; set; }
        public string Course { private get; set; }
        public string Assignments { private get; set; }
        public int OralMark { private get; set; }
        public int TotalMark { private get; set; }
    }
    class StudentInMoreCourses
    {
        public int StudentId { private get; set; }
        public string Name { private get; set; }
        public int NumberOfCourses { private get; set; }
    }
    class XInCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}
