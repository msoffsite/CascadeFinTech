using FluentValidation;

namespace CascadeFinTech.Application.Orders.PlaceCustomerOrder
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            this.RuleFor(x => x.Quantity).GreaterThan(0)
                .WithMessage("At least one book has invalid quantity");
        }
    }
}