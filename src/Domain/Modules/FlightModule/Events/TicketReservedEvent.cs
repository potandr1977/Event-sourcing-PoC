using Domain.Core;
using Domain.TicketModule;

namespace Domain.Modules.FlightModule.Events
{
    public class TicketReservedEvent : DomainEventBase<FlightId>
    {
        public TicketId TicketId { get; init; }
        public int Quantity { get; init; }

        TicketReservedEvent()
        {
        }

        internal TicketReservedEvent(TicketId ticketId, int quantity) : base()
        {
            TicketId = ticketId;
            Quantity = quantity;
        }

        internal TicketReservedEvent(FlightId aggregateId, long aggregateVersion, TicketId ticketId, int quantity) : base(aggregateId, aggregateVersion)
        {
            TicketId = ticketId;
            Quantity = quantity;
        }
        

        public override IDomainEvent<FlightId> WithAggregate(FlightId aggregateId, long aggregateVersion)
        {
            return new TicketReservedEvent(aggregateId, aggregateVersion, TicketId, Quantity);
        }
    }
}
