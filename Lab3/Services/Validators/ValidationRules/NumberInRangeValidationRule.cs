namespace Lab3.Services.Validators.ValidationRules
{
    public class NumberInRangeValidationRule : ValidationRuleBase<double>
    {
        private readonly double _minValue;
        private readonly double _maxValue;


        public NumberInRangeValidationRule(string propertyName, double minValue, double maxValue, string errorMessage)
            : base(propertyName, errorMessage)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }


        protected override bool ValidateValue(double value)
        {
            return value >= _minValue && value <= _maxValue;
        }
    }
}