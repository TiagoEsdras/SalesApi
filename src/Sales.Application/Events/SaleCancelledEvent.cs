using Sales.Application.Events.Enum;

namespace Sales.Application.Events
{
    public class SaleCancelledEvent : Event<Guid>
    {
        public SaleCancelledEvent(Guid saleId) : base(EventType.SaleCancelled, saleId)
        {
        }
    }
}