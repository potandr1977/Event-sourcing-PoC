using ReadModel.Common;

namespace ReadModel.Modules.Ticket
{
    public record Ticket : IReadEntity
    {
        public string Id { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }
    }
}
