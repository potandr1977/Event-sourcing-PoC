using Domain.Core;
using Domain.FlightModule;
using Domain.JetModule;
using Domain.Modules.FlightModule;
using Domain.Modules.FlightModule.Events;
using Domain.TicketModule;

namespace Domain
{
    public class Flight : AggregateBase<FlightId>
    {
        public const int SeatQuantityThreshold = 50;
        private JetId JetId { get; set; }
        private List<Seat> Items { get; set; }


        //Needed for persistence purposes
        private Flight()
        {
            Items = new List<Seat>();
        }
        
        public Flight(FlightId flightId, JetId jetId) : this()
        {
            if (flightId == null)
            {
                throw new ArgumentNullException(nameof(FlightId));
            }

            if (jetId == null)
            {
                throw new ArgumentNullException(nameof(jetId));
            }

            RaiseEvent(new FlightCreatedEvent(flightId, jetId));
        }

        public void AddProduct(TicketId ticketId, int quantity)
        {
            if (ticketId == null)
            {
                throw new ArgumentNullException(nameof(ticketId));
            }
            if (ContainsTicket(ticketId))
            {
                throw new FlightException($"Ticket {ticketId} already added");
            }
            CheckQuantity(ticketId, quantity);
            RaiseEvent(new TicketReservedEvent(ticketId, quantity));
        }

        public void ChangeQuantity(TicketId ticketId, int quantity)
        {
            if (!ContainsTicket(ticketId))
            {
                throw new FlightException($"Product {ticketId} not found");
            }
            CheckQuantity(ticketId, quantity);
            RaiseEvent(new TicketReservationCancelledEvent(ticketId, GetFlightItemByProduct(ticketId).Quantity, quantity));
        }

        public override string ToString()
        {
            return $"{{ Id: \"{Id}\", JetId: \"{JetId.IdAsString()}\", Items: [{string.Join(", ", Items.Select(x => x.ToString()))}] }}";
        }

        internal void Apply(FlightCreatedEvent ev)
        {
            Id = ev.AggregateId;
            JetId = ev.JetId;
        }

        internal void Apply(TicketReservedEvent ev)
        {
            Items.Add(new Seat(ev.TicketId, ev.Quantity));
        }

        internal void Apply(TicketReservationCancelledEvent ev)
        {
            var existingItem = Items.Single(x => x.TicketId == ev.TicketId);

            Items.Remove(existingItem);
            Items.Add(existingItem.WithQuantity(ev.NewQuantity));
        }

        private bool ContainsTicket(TicketId ticketId)
        {
            return Items.Any(x => x.TicketId == ticketId);
        }

        private Seat GetFlightItemByProduct(TicketId ticketId)
        {
            return Items.Single(x => x.TicketId == ticketId);
        }

        private static void CheckQuantity(TicketId ticketId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            }
            if (quantity > SeatQuantityThreshold)
            {
                throw new FlightException($"Quantity of seats for ticket {ticketId} must be less than or equal to {SeatQuantityThreshold}");
            }
        }
    }
}