using Domain.Core;

namespace Domain.TicketModule
{
    public class TicketId : IAggregateId
    {
        private const string IdAsStringPrefix = "Ticket-";

        public Guid Id { get; private set; }

        private TicketId(Guid id)
        {
            Id = id;
        }

        public TicketId(string id)
        {
            Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
        }

        public override string ToString()
        {
            return IdAsString();
        }

        public override bool Equals(object obj)
        {
            return obj is TicketId && Equals(Id, ((TicketId)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static TicketId NewProductId()
        {
            return new TicketId(Guid.NewGuid());
        }

        public string IdAsString()
        {
            return $"{IdAsStringPrefix}{Id.ToString()}";
        }

        public static bool operator !=(TicketId left, TicketId right)
        {
            return !(left == right);
        }

        public static bool operator ==(TicketId left, TicketId right)
        {
            return Equals(left?.Id, right?.Id);
        }
    }
}
