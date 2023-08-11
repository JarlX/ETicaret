namespace ETicaret.API.Aspects;

using Microsoft.AspNetCore.Mvc.Filters;
using Validation.FluentValidation;

public class ValidationFilter : Attribute , IAsyncActionFilter
{
    private Type _validatorType;

    public ValidationFilter(Type validatorType)
    {
        _validatorType = validatorType;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ValidationHelper.Validate(_validatorType,context.ActionArguments.Values.ToArray());
        await next();
    }
}