using System;
using Lab3.ViewModels;

namespace Lab3.Services
{
    public interface IStudentForm
    {
        event EventHandler<StudentEventArgs> StudentSubmitted;


        void EnableDisableSubmit(bool enable);
    }
}