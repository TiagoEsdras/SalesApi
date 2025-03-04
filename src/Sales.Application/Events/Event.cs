using Sales.Application.Events.Enum;

namespace Sales.Application.Events
{
    public abstract class Event<T>
    {
        public EventType EventType { get; private set; }
        public DateTime EventDate { get; private set; }
        public T Data { get; private set; }

        protected Event(EventType eventType, T data)
        {
            EventType = eventType;
            EventDate = DateTime.UtcNow;
            Data = data;
        }
    }
}