using FluentValidation;
using Sales.Application.Commands.Sales;
using Sales.Application.Shared;

namespace Sales.Application.Validators.Sales
{
    public class SaleItemCommandValidator : AbstractValidator<SaleItemCommand>
    {
        public SaleItemCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(SaleItemCommand.ProductId)));

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage(string.Format(Consts.FieldMustBeGreaterThan, nameof(SaleItemCommand.Quantity), 0))
                .LessThanOrEqualTo(20)
                .WithMessage(string.Format(Consts.FieldMustBeLowerOrEqualTo, nameof(SaleItemCommand.Quantity), 20));

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage(string.Format(Consts.FieldMustBeGreaterThan, nameof(SaleItemCommand.UnitPrice), 0));

            RuleFor(x => x.TotalPrice)
               .GreaterThan(0)
               .WithMessage(string.Format(Consts.FieldMustBeGreaterThan, nameof(SaleItemCommand.TotalPrice), 0));
        }
    }
}