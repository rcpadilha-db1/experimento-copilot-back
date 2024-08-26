using FluentValidation;
using FluentValidation.Results;

namespace Domain._Base.Validators;

public class BaseValidator<T> : AbstractValidator<T>
{
    protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
    {
        if (context.InstanceToValidate is not null) 
            return true;
        
        result.Errors.Add(new ValidationFailure(string.Empty, "Parâmetro de entrada está nulo ou inválido"));
        return false;
    }
}