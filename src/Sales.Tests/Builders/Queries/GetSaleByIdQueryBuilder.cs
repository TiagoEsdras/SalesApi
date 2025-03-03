using Sales.Application.Queries.Sales;

namespace Sales.Tests.Builders.Queries
{
    public class GetSaleByIdQueryBuilder
    {
        private readonly GetSaleByIdQuery _instance;

        public GetSaleByIdQueryBuilder()
        {
            _instance = new GetSaleByIdQuery(Guid.NewGuid());
        }

        public GetSaleByIdQueryBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public GetSaleByIdQuery Build() => _instance;
    }
}