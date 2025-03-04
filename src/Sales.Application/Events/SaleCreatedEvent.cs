using Sales.Application.Events.Enum;
using Sales.Domain.Entities;

namespace Sales.Application.Events
{
    public class SaleCreatedEvent : Event<Sale>
    {
        public SaleCreatedEvent(Sale sale) : base(EventType.SaleCreated, sale)
        {
        }
    }
}