using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Lab3.Services.Validators;
using Lab3.Services.Validators.ValidationRules;
using Prism.Commands;
using Prism.Mvvm;

namespace Lab3.ViewModels
{
    public abstract class ValidatableViewModelBase : BindableBase,  IDataErrorInfo
    {
        private IValidator _validator;

        private readonly DelegateCommand _submitCommand;
        private string _error;
        private bool _isSubmitEnabled;


        public string Error
        {
            get => _error;
            private set
            {
                SetProperty(ref _error, value);
                _submitCommand.RaiseCanExecuteChanged();
            }
        }

        public string this[string columnName] => Error = ValidateAndUpdate(columnName);


        private IValidator Validator => _validator ?? (_validator = new Validator(this, ResolveValidatorRules()));


        public ICommand SubmitCommand => _submitCommand;


        protected ValidatableViewModelBase()
        {
            _submitCommand = new DelegateCommand(Submit, () => _isSubmitEnabled && Validator.CheckIfAllValid());
            _isSubmitEnabled = true;
        }


        public void EnableDisableSubmit(bool enable)
        {
            if (_isSubmitEnabled == enable)
            {
                return;
            }

            _isSubmitEnabled = enable;
            _submitCommand.RaiseCanExecuteChanged();
        }


        protected abstract IEnumerable<IValidationRule> ResolveValidatorRules();

        protected abstract void Submit();


        private string ValidateAndUpdate(string columnName)
        {
            var errors = Validator.Validate(columnName);

            return errors.FirstOrDefault();
        }
    }
}