namespace ETicaret.API.Validation.FluentValidation;

using global::FluentValidation;
using Helper.CustomException;

public static class ValidationHelper
{
    public static void Validate(Type type,object?[] items)
    {
        if (!typeof(IValidator).IsAssignableFrom(type))
        {
            throw new Exception("Hata Oluştu. Verilen Tip Validator Tipi Değildir");
        }

        var validator = (IValidator)Activator.CreateInstance(type);

        foreach (var item in items)
        {
            if (validator.CanValidateInstancesOfType(item.GetType()))
            {
                var result = validator.Validate(new ValidationContext<object>(item));

                List<string> validationMessages = new();

                foreach (var validationFailure in result.Errors)
                {
                    validationMessages.Add(validationFailure.ErrorMessage);
                }

                if (!result.IsValid)
                {
                    throw new FieldValidationException(validationMessages);
                }
            }
            
        }
    }
}