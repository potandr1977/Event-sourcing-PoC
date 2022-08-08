using Domain.Core;

namespace Domain.Modules.FlightModule
{
    public class FlightId : IAggregateId
    {
        private const string IdAsStringPrefix = "Flight-";

        public Guid Id { get; private set; }

        private FlightId(Guid id)
        {
            Id = id;
        }

        public FlightId(string id)
        {
            Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
        }

        public override string ToString()
        {
            return IdAsString();
        }

        public override bool Equals(object obj)
        {
            return obj is FlightId && Equals(Id, ((FlightId)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static FlightId NewFlightId()
        {
            return new FlightId(Guid.NewGuid());
        }

        public string IdAsString()
        {
            return $"{IdAsStringPrefix}{Id.ToString()}";
        }
    }
}
