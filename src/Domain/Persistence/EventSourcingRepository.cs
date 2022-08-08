﻿using Domain.Core;
using Domain.Persistence.EventStore;
using System.Reflection;

namespace Domain.Persistence
{
    public class EventSourcingRepository<TAggregate, TAggregateId> : IRepository<TAggregate, TAggregateId>
        where TAggregate : AggregateBase<TAggregateId>, IAggregate<TAggregateId>
        where TAggregateId : IAggregateId
    {
        private readonly IEventStore eventStore;
        private readonly IEventPublisher eventPublisher;

        public EventSourcingRepository(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            this.eventStore = eventStore;
            this.eventPublisher = eventPublisher;
        }

        public async Task<TAggregate> GetByIdAsync(TAggregateId id)
        {
            try
            {
                var aggregate = CreateEmptyAggregate();
                IEventSourcingAggregate<TAggregateId> aggregatePersistence = aggregate;

                foreach (var @event in await eventStore.ReadEventsAsync(id))
                {
                    aggregatePersistence.ApplyEvent(@event.DomainEvent, @event.EventNumber);
                }
                return aggregate;
            }
            catch (EventStoreAggregateNotFoundException)
            {
                return null;
            }
            catch (EventStoreCommunicationException ex)
            {
                throw new RepositoryException("Unable to access persistence layer", ex);
            }
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            try
            {
                IEventSourcingAggregate<TAggregateId> aggregatePersistence = aggregate;

                foreach (var @event in aggregatePersistence.GetUncommittedEvents())
                {
                    await eventStore.AppendEventAsync(@event);
                    await eventPublisher.PublishAsync((dynamic)@event);
                }
                aggregatePersistence.ClearUncommittedEvents();
            }
            catch (EventStoreCommunicationException ex)
            {
                throw new RepositoryException("Unable to access persistence layer", ex);
            }
        }

        private TAggregate CreateEmptyAggregate()
        {
            return (TAggregate)typeof(TAggregate)
                    .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, 
                        null, new Type[0], new ParameterModifier[0])
                    .Invoke(new object[0]);
        }
    }
}
