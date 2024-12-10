using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;
public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(order => order.Id)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage("Id is required.")
                 .GreaterThan(0).WithMessage("Id must be greater than zero."); ;

        RuleFor(order => order.UserName)
                    .NotEmpty().WithMessage("UserName is required.")
                    .MaximumLength(70).WithMessage("UserName cannot exceed 50 characters.");

        RuleFor(order => order.TotalPrice)
            .GreaterThan(-1).WithMessage("TotalPrice must be greater than -1.");

        RuleFor(order => order.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters.");

        RuleFor(order => order.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(50).WithMessage("LastName cannot exceed 50 characters.");

        RuleFor(order => order.EmailAddress)
            .NotEmpty().WithMessage("EmailAddress is required.")
            .EmailAddress().WithMessage("EmailAddress must be a valid email address.");

        RuleFor(order => order.AddressLine)
            .NotEmpty().WithMessage("AddressLine is required.");

        RuleFor(order => order.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(order => order.State)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(order => order.ZipCode)
            .NotEmpty().WithMessage("ZipCode is required.")
            .Matches(@"^\d{5}(-\d{4})?$").WithMessage("ZipCode must be a valid postal code.");

        RuleFor(order => order.CardName)
            .NotEmpty().WithMessage("CardName is required.");

        RuleFor(order => order.CardNumber)
            .NotEmpty().WithMessage("CardNumber is required.")
            .CreditCard().WithMessage("CardNumber must be a valid credit card number.");

        RuleFor(order => order.Expiration)
            .NotEmpty().WithMessage("Expiration is required.")
            .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$")
            .WithMessage("Expiration must be in MM/YY format.");

        RuleFor(order => order.Cvv)
            .NotEmpty().WithMessage("CVV is required.")
            .Matches(@"^\d{3,4}$").WithMessage("CVV must be 3 or 4 digits.");

        RuleFor(order => order.PaymentMethod)
            .NotNull().WithMessage("PaymentMethod is required.")
            .InclusiveBetween(1, 3).WithMessage("PaymentMethod must be a valid option (1, 2, or 3).");
    }
}
