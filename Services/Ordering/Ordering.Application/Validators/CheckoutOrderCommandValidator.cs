using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;
public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(user => user.UserName)
             .NotEmpty().WithMessage("UserName is required.")
             .MaximumLength(70).WithMessage("UserName cannot exceed 50 characters.");

        RuleFor(user => user.TotalPrice)
            .GreaterThan(0).WithMessage("TotalPrice must be greater than zero.");

        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters.");

        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(50).WithMessage("LastName cannot exceed 50 characters.");

        RuleFor(user => user.EmailAddress)
            .NotEmpty().WithMessage("EmailAddress is required.")
            .EmailAddress().WithMessage("EmailAddress must be a valid email address.");

        RuleFor(user => user.AddressLine)
            .NotEmpty().WithMessage("AddressLine is required.");

        RuleFor(user => user.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(user => user.State)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(user => user.ZipCode)
            .NotEmpty().WithMessage("ZipCode is required.")
            .Matches(@"^\d{5}(-\d{4})?$").WithMessage("ZipCode must be a valid postal code.");

        RuleFor(user => user.CardName)
            .NotEmpty().WithMessage("CardName is required.");

        RuleFor(user => user.CardNumber)
            .NotEmpty().WithMessage("CardNumber is required.")
            .CreditCard().WithMessage("CardNumber must be a valid credit card number.");

        RuleFor(user => user.Expiration)
            .NotEmpty().WithMessage("Expiration is required.")
            .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$")
            .WithMessage("Expiration must be in MM/YY format.");

        RuleFor(user => user.Cvv)
            .NotEmpty().WithMessage("CVV is required.")
            .Matches(@"^\d{3,4}$").WithMessage("CVV must be 3 or 4 digits.");

        RuleFor(user => user.PaymentMethod)
            .NotNull().WithMessage("PaymentMethod is required.")
            .InclusiveBetween(1, 3).WithMessage("PaymentMethod must be a valid option (1, 2, or 3).");
    }
}
