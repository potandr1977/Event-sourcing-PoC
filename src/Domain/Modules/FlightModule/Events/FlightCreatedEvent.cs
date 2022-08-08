using Domain.Core;
using Domain.JetModule;

namespace Domain.Modules.FlightModule.Events
{
    public class FlightCreatedEvent : DomainEventBase<FlightId>
    {
        public JetId JetId { get; init; }

        FlightCreatedEvent()
        {
        }

        internal FlightCreatedEvent(FlightId aggregateId, JetId jetId) : base(aggregateId)
        {
            JetId = jetId;
        }

        private FlightCreatedEvent(FlightId aggregateId, long aggregateVersion, JetId jetId) : base(aggregateId, aggregateVersion)
        {
            JetId = jetId;
        }

        public override IDomainEvent<FlightId> WithAggregate(FlightId aggregateId, long aggregateVersion)
        {
            return new FlightCreatedEvent(aggregateId, aggregateVersion, JetId);
        }
    }
}
