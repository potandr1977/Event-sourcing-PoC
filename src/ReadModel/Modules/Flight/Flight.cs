using ReadModel.Common;

namespace ReadModel.Flight
{
    public record Flight : IReadEntity
    {
        public string Id { get; set; }

        public int TotalBoughtTickets { get; set; }

        public string JetId { get; set; }

        public string JetName { get; set; }
    }
}
