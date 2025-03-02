using FluentValidation;
using Sales.Application.Commands.Products;
using Sales.Application.Shared;

namespace Sales.Application.Validators.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Title)
                 .NotEmpty()
                 .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateProductCommand.Title)));

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage(string.Format(Consts.FieldMustBeGreaterThan, nameof(CreateProductCommand.Price), 0));

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateProductCommand.Description)));

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateProductCommand.Category)));

            RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage(string.Format(Consts.FieldCannotBeNullOrEmpty, nameof(CreateProductCommand.Image)));
        }
    }
}