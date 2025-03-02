using FluentValidation;
using Sales.Application.Queries;
using Sales.Application.Shared;

namespace Sales.Application.Validators.Shared
{
    public class QueryByIdValidator<TQuery> : AbstractValidator<TQuery> where TQuery : IQueryById
    {
        public QueryByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(Consts.GuidCannotBeEmptyGuid, nameof(IQueryById.Id)));
        }
    }
}