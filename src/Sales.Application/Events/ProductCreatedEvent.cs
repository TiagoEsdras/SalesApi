using Sales.Application.Events.Enum;
using Sales.Domain.Entities;

namespace Sales.Application.Events
{
    public class ProductCreatedEvent : Event<Product>
    {
        public ProductCreatedEvent(Product product) : base(EventType.ProductCreated, product)
        {
        }
    }
}