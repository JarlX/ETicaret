namespace ETicaret.Helper.CustomException;

public class FieldValidationException : Exception
{
    public FieldValidationException(List<string> validationMessages)
    {
        base.Data["FieldValidationErrors"] = validationMessages;
    }
}