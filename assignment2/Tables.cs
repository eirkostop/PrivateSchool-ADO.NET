using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment2
{
    class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal TuitionFees { get; set; }
    }
    class Trainer
    {
        public int TrainerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string subject { private get; set; }
    }
    class Assignment
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
    class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Stream { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    class StudentPerCourse
    {
        public int CourseId { get; set; }
        public string Course { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; }
    }
    class TrainerPerCourse
    {
        public int CourseId { get; set; }
        public string Course { private get; set; }
        public int TrainerId { get; set; }
        public string Name { get; set; }
    }
    class AssignmentPerCourse
    {
        public int CourseId { get; set; }
        public string Course { get; set; }
        public int AssignmentId { get; set; }
        public string Assignment { get; set; }
    }
    class AssignmentPerCoursePerStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string Assignments { get; set; }
        public int OralMark { get; set; }
        public int TotalMark { get; set; }
    }
    class StudentInMoreCourses
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int NumberOfCourses { get; set; }
    }
    class XInCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}
