using System;
using Lab2.BL.Models;

namespace Lab3.ViewModels
{
    public class StudentEventArgs : EventArgs
    {
        public StudentModel Student { get; }


        public StudentEventArgs(StudentModel student)
        {
            Student = student;
        }
    }
}