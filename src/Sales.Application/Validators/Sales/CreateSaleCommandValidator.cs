using FluentValidation;
using Sales.Application.Commands.Sales;
using Sales.Application.Shared;

namespace Sales.Application.Validators.Sales
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator(IValidator<SaleItemCommand> saleItemCommandValidator)
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateSaleCommand.SaleNumber)));

            RuleFor(x => x.SaleDate)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateSaleCommand.SaleDate)));

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateSaleCommand.CustomerId)));

            RuleFor(x => x.BranchId)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateSaleCommand.BranchId)));

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateSaleCommand.Items)));

            When(x => x.Items is not null, () =>
            {
                RuleFor(t => t).Custom((request, ctx) =>
                {
                    var duplicateProductIds = request.Items!
                        .GroupBy(i => i.ProductId)
                        .Where(g => g.Count() > 1)
                        .Select(g => g.Key)
                        .ToList();

                    if (duplicateProductIds.Count != 0)
                    {
                        ctx.AddFailure(nameof(request.Items), string.Format(Consts.DuplicatedProductIds, string.Join(", ", duplicateProductIds)));
                    }
                });

                RuleForEach(x => x.Items).SetValidator(saleItemCommandValidator);
            });
        }
    }
}