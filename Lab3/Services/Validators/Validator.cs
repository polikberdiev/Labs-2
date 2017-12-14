using System;
using System.Collections.Generic;
using System.Linq;
using Lab3.Services.Validators.ValidationRules;

namespace Lab3.Services.Validators
{
    public class Validator : IValidator
    {
        private readonly object _target;
        private readonly IEnumerable<IValidationRule> _validationRules;


        public Validator(object target, IEnumerable<IValidationRule> validationRules)
        {
            _target = target;
            _validationRules = validationRules.Select(ThrowIfPropertyNotExists).ToList();
        }


        public IEnumerable<string> Validate(string propertyName)
        {
            return _validationRules
                .Where(r => r.PropertyName.Equals(propertyName)
                    && !r.Validate(GetPropertyValue(propertyName)))
                .Select(r => r.ErrorMessage);
        }

        public bool CheckIfAllValid()
        {
            return _validationRules
                .All(r => r.Validate(GetPropertyValue(r.PropertyName)));
        }


        private object GetPropertyValue(string propertyName)
        {
            // ReSharper disable once PossibleNullReferenceException
            return _target.GetType().GetProperty(propertyName).GetValue(_target);
        }

        private IValidationRule ThrowIfPropertyNotExists(IValidationRule validationRule)
        {
            var type = _target.GetType();
            var property = type.GetProperty(validationRule.PropertyName);
            if (property == null)
            {
                throw new ArgumentNullException(
                    nameof(property),
                    $@"Type ""{type.Name}"" not contains the ""{validationRule.PropertyName}"" property.");
            }

            return validationRule;
        }
    }
}