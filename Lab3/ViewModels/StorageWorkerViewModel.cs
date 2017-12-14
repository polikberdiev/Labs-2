using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Lab2.BL.Models;
using Lab2.BL.Services;
using Lab3.Services;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;

namespace Lab3.ViewModels
{
    public class StorageWorkerViewModel : BindableBase
    {
        private readonly IStorageFactory _storageFactory;
        private readonly ISortStudentsForm _sortStudentsForm;
        private readonly IStudentForm _studentForm;

        private readonly DelegateCommand _saveStudentsCommand;
        private readonly ObservableCollection<StudentModel> _students;
        private IStorage<StudentModel> _storage;


        public IEnumerable<StudentModel> Students => _students;

        public bool IsStorageOpened => _storage != null;


        public ICommand CreateStorageCommand { get; }

        public ICommand OpenStorageCommand { get; }

        public ICommand SaveStudentsCommand => _saveStudentsCommand;


        public StorageWorkerViewModel(IStorageFactory storageFactory, ISortStudentsForm sortStudentsForm, IStudentForm studentForm)
        {
            _storageFactory = storageFactory;
            _sortStudentsForm = sortStudentsForm;
            _studentForm = studentForm;

            CreateStorageCommand = new DelegateCommand(CreateStorage);
            OpenStorageCommand = new DelegateCommand(OpenStorage);
            _saveStudentsCommand = new DelegateCommand(SaveStudents, () => _storage != null);

            _students = new ObservableCollection<StudentModel>();

            _sortStudentsForm.SearchSubmitted += SortStudentsFormOnSearchSubmitted;
            _studentForm.StudentSubmitted += (sender, e) => _storage.Add(e.Student);
        }


        private void SortStudentsFormOnSearchSubmitted(object sender, SortStudentsEventArgs e)
        {
            var sortedStudents = _storage.GetSortedBy(e.PropertyName, e.IsAsc, e.Count);

            _students.Clear();
            _students.AddRange(sortedStudents);
        }

        private void CreateStorage()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Data file|*.dat"
            };
            var result = saveFileDialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            try
            {
                _storage = _storageFactory.CreateFileStorageViewModel<StudentModel>(saveFileDialog.FileName);
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.");
            }
        }

        private void OpenStorage()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            try
            {
                _storage = _storageFactory.CreateFileStorageViewModel<StudentModel>(openFileDialog.FileName);
                UpdateState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.");
            }
        }

        private void SaveStudents()
        {
            _storage.Save();
        }

        private void UpdateState()
        {
            _saveStudentsCommand.RaiseCanExecuteChanged();
            _sortStudentsForm.EnableDisableSubmit(IsStorageOpened);
            _studentForm.EnableDisableSubmit(IsStorageOpened);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStorageOpened)));
        }
    }
}