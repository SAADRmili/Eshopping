using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;
public class CheckoutOrderCommandValidatorV2 : AbstractValidator<CheckoutOrderCommandV2>
{
    public CheckoutOrderCommandValidatorV2()
    {
        RuleFor(user => user.UserName)
                  .NotEmpty().WithMessage("UserName is required.")
                  .MaximumLength(70).WithMessage("UserName cannot exceed 50 characters.");

        RuleFor(user => user.TotalPrice)
            .GreaterThan(0).WithMessage("TotalPrice must be greater than zero.");
    }
}
