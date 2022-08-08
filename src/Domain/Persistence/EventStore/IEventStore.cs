using Domain.Core;

namespace Domain.Persistence.EventStore
{
    public interface IEventStore
    {
        Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id)
            where TAggregateId: IAggregateId;

        Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> @event)
            where TAggregateId: IAggregateId;
    }
}
