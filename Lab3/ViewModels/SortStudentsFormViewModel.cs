using System;
using System.Collections.Generic;
using System.Linq;
using Lab2.BL.Models;
using Lab3.Services;
using Lab3.Services.Validators.ValidationRules;

namespace Lab3.ViewModels
{
    public class SortStudentsFormViewModel : ValidatableViewModelBase, ISortStudentsForm
    {
        private readonly IDictionary<string, string> _propertyNames;
        private string _selectedSortByProperty;
        private bool _isDescSort;
        private string _count;


        public string SelectedSortByProperty
        {
            get => _selectedSortByProperty;
            set => SetProperty(ref _selectedSortByProperty, value);
        }

        public IEnumerable<string> SortByPropertyNames => _propertyNames.Keys;

        public bool IsDescSort
        {
            get => _isDescSort;
            set => SetProperty(ref _isDescSort, value);
        }

        public string Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }


        public event EventHandler<SortStudentsEventArgs> SearchSubmitted;


        public SortStudentsFormViewModel()
        {
            _propertyNames = new Dictionary<string, string>
            {
                { "First name", nameof(StudentModel.FirstName) },
                { "Last name", nameof(StudentModel.LastName) },
                { "Test title", nameof(StudentModel.TestTitle) },
                { "Rating", nameof(StudentModel.Rating) },
                { "Test pass date", nameof(StudentModel.Timestamp) }
            };
            SelectedSortByProperty = SortByPropertyNames.First();
            Count = 10.ToString();
        }


        protected override IEnumerable<IValidationRule> ResolveValidatorRules()
        {
            return new IValidationRule[]
            {
                new CustomValidationRule<string>(nameof(SelectedSortByProperty), o => SortByPropertyNames.Contains(o), "Sort by field should be provided correctly."),
                new NotEmptyStringValidationRule(nameof(Count), "Count field should be filled."),
                new Int32ValidationRule(nameof(Count), "Count should be a number.")
            };
        }

        protected override void Submit()
        {
            SearchSubmitted?.Invoke(this,
                new SortStudentsEventArgs(_propertyNames[SelectedSortByProperty], !IsDescSort, Int32.Parse(Count)));
        }
    }
}