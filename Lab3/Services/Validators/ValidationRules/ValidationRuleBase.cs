using System;

namespace Lab3.Services.Validators.ValidationRules
{
    public abstract class ValidationRuleBase<TProperty> : IValidationRule
    {
        public string PropertyName { get; }

        public string ErrorMessage { get; }


        protected ValidationRuleBase(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }


        public bool Validate(object value)
        {
            TProperty converted;

            try
            {
                converted = (TProperty) Convert.ChangeType(value, typeof(TProperty));
            }
            catch (FormatException)
            {
                return false;
            }

            var isValid = ValidateValue(converted);

            return isValid;
        }


        protected virtual bool ValidateValue(TProperty value)
        {
            return true;
        }
    }
}