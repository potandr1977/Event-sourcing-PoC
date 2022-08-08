using Domain.TicketModule;

namespace Domain.Modules.FlightModule
{
    public class Seat
    {
        public Seat(TicketId ticketId, int quantity)
        {
            TicketId = ticketId;
            Quantity = quantity;
        }

        public TicketId TicketId { get; }

        public int Quantity { get; }

        public override bool Equals(object obj)
        {
            var castedObj = obj as Seat;
            return castedObj != null && Equals(castedObj.TicketId, TicketId)
                && Equals(castedObj.Quantity, Quantity);
        }

        public override int GetHashCode()
        {
            var hashCode = 76325633;
            hashCode = hashCode * -1521134295 + TicketId.GetHashCode();
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{{ TicketId: \"{TicketId}\", Quantity: {Quantity} }}";
        }

        public Seat WithQuantity(int quantity)
        {
            return new Seat(TicketId, quantity);
        }
    }
}
