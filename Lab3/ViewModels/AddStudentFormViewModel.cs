using System;
using System.Collections.Generic;
using Lab2.BL.Models;
using Lab3.Services;
using Lab3.Services.Validators.ValidationRules;

namespace Lab3.ViewModels
{
    public class AddStudentFormViewModel : ValidatableViewModelBase, IStudentForm
    {
        private string _firstName;
        private string _lastName;
        private string _testTitle;
        private string _rating;


        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string TestTitle
        {
            get => _testTitle;
            set => SetProperty(ref _testTitle, value);
        }

        public string Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }


        public event EventHandler<StudentEventArgs> StudentSubmitted;


        protected override IEnumerable<IValidationRule> ResolveValidatorRules()
        {
            return new IValidationRule[]
            {
                new NotEmptyStringValidationRule(nameof(FirstName), "First name field should be filled."),
                new NotEmptyStringValidationRule(nameof(LastName), "Last name field should be filled."),
                new NotEmptyStringValidationRule(nameof(TestTitle), "Test title field should be filled."),
                new NotEmptyStringValidationRule(nameof(Rating), "Rating field should be filled."),
                new Int32ValidationRule(nameof(Rating), "Rating should be a number."),
                new NumberInRangeValidationRule(nameof(Rating), 0, 10, "Rating should be in range 0-10.")
            };
        }


        protected override void Submit()
        {
            var student = new StudentModel
            {
                FirstName = FirstName,
                LastName = LastName,
                TestTitle = TestTitle,
                Timestamp = DateTime.UtcNow,
                Rating = Int32.Parse(Rating)
            };

            FirstName = null;
            LastName = null;
            TestTitle = null;
            Rating = null;

            StudentSubmitted?.Invoke(this, new StudentEventArgs(student));
        }
    }
}