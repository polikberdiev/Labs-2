using System.Collections.Generic;

namespace Lab3.Services.Validators
{
    public interface IValidator
    {
        IEnumerable<string> Validate(string propertyName);

        bool CheckIfAllValid();
    }
}