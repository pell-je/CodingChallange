using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace JobTargetCodingChallange.Validation
{
    public class ValidationService
    {

        public static bool EnitityIsValid<T>(T obj, out List<ValidationResult> validationResults)
        {
            if (obj == null) {
                validationResults = [];
                return true; 
            }
            validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj, null, null);
            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }

        public static void ThrowIfEntityIsInvalid<T>(T obj)
        {
            if (obj == null)
            {
                return;
            }

            if (!EnitityIsValid(obj, out var results)) {
                throw new ValidationException($"{obj.GetType()} is not valid: {JsonSerializer.Serialize(results)}");
            }
        }
    }
}
