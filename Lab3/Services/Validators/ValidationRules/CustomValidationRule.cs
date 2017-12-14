using System;

namespace Lab3.Services.Validators.ValidationRules
{
    public class CustomValidationRule<TPropery> : ValidationRuleBase<TPropery>
    {
        private readonly Func<TPropery, bool> _validateFunc;


        public CustomValidationRule(string propertyName, Func<TPropery, bool> validateFunc, string errorMessage)
            : base(propertyName, errorMessage)
        {
            _validateFunc = validateFunc;
        }


        protected override bool ValidateValue(TPropery value)
        {
            return _validateFunc(value);
        }
    }
}