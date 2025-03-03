using Sales.Application.Queries.Sales;

namespace Sales.Tests.Builders.Queries
{
    public class CancelSaleByIdQueryBuilder
    {
        private readonly CancelSaleByIdQuery _instance;

        public CancelSaleByIdQueryBuilder()
        {
            _instance = new CancelSaleByIdQuery(Guid.NewGuid());
        }

        public CancelSaleByIdQueryBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public CancelSaleByIdQuery Build() => _instance;
    }
}