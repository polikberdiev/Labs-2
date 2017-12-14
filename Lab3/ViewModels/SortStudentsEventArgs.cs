using System;

namespace Lab3.ViewModels
{
    public class SortStudentsEventArgs : EventArgs
    {
        public string PropertyName { get; }

        public bool IsAsc { get; }

        public int Count { get; }


        public SortStudentsEventArgs(string propertyName, bool isAsc, int count)
        {
            PropertyName = propertyName;
            IsAsc = isAsc;
            Count = count;
        }
    }
}