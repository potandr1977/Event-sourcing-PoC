using ReadModel.Common;

namespace ReadModel.Flight
{
    public record Seat : IReadEntity
    {
        public string Id { get; private set; }

        public string FlightId { get; set; }

        public string TicketId { get; set; }

        public string TicketName { get; set; }

        public int Quantity { get; set; }

        public static Seat CreateFor(string flightId, string ticketId)
        {
            return new Seat
            {
                Id = IdFor(flightId, ticketId),
                FlightId = flightId,
                TicketId = ticketId
            };
        }

        public static string IdFor(string flightId, string ticketId)
        {
            return $"{ticketId}@{flightId}";
        }
    }
}
