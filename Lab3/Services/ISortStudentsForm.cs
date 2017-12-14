using System;
using Lab3.ViewModels;

namespace Lab3.Services
{
    public interface ISortStudentsForm
    {
        event EventHandler<SortStudentsEventArgs> SearchSubmitted;


        void EnableDisableSubmit(bool enable);
    }
}