using Domain.Core;
using Domain.TicketModule;

namespace Domain.Modules.FlightModule.Events
{
    public class TicketReservationCancelledEvent : DomainEventBase<FlightId>
    {
        public TicketId TicketId { get; private set; }
        public int OldQuantity { get; private set; }
        public int NewQuantity { get; private set; }

        TicketReservationCancelledEvent()
        {
        }

        internal TicketReservationCancelledEvent(TicketId ticketId, int oldQuantity, int newQuantity) : base()
        {
            TicketId = ticketId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }

        private TicketReservationCancelledEvent(FlightId aggregateId, long aggregateVersion, TicketId ticketId,
            int oldQuantity, int newQuantity) : base(aggregateId, aggregateVersion)
        {
            TicketId = ticketId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }

        public override IDomainEvent<FlightId> WithAggregate(FlightId aggregateId, long aggregateVersion)
        {
            return new TicketReservationCancelledEvent(aggregateId, aggregateVersion, TicketId, OldQuantity, NewQuantity);
        }
    }
}
