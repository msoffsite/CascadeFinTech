using FluentValidation;

namespace CascadeFinTech.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommandValidator : AbstractValidator<PlaceCustomerOrderCommand>
    {
        public PlaceCustomerOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty");
            RuleFor(x => x.Books).NotEmpty().WithMessage("Books list is empty");
            RuleForEach(x => x.Books).SetValidator(new BookDtoValidator());

            this.RuleFor(x => x.Currency).Must(x => x == "USD" || x == "EUR")
                .WithMessage("At least one book has invalid currency");
        }
    }
}