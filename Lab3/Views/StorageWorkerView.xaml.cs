using Lab3.Services;
using Lab3.ViewModels;

namespace Lab3.Views
{
    public partial class StorageWorkerView
    {
        public StorageWorkerView()
        {
            InitializeComponent();

            var addStudentFormViewModel = new AddStudentFormViewModel();
            var sortStudentsFormViewModel = new SortStudentsFormViewModel();

            sortStudentsFormView.DataContext = sortStudentsFormViewModel;
            addStudentFormView.DataContext = addStudentFormViewModel;
            DataContext = new StorageWorkerViewModel(new StorageFactory(), sortStudentsFormViewModel, addStudentFormViewModel);
        }
    }
}