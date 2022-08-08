using Domain.Core;

namespace Domain.JetModule
{
    public class JetId : IAggregateId
    {
        private const string IdAsStringPrefix = "Jet-";

        public Guid Id { get; private set; }

        private JetId(Guid id)
        {
            Id = id;
        }

        public JetId(string id)
        {
            Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
        }

        public override string ToString()
        {
            return IdAsString();
        }

        public override bool Equals(object obj)
        {
            return obj is JetId && Equals(Id, ((JetId)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static JetId NewJetId()
        {
            return new JetId(Guid.NewGuid());
        }

        public string IdAsString()
        {
            return $"{IdAsStringPrefix}{Id.ToString()}";
        }

        public static bool operator !=(JetId left, JetId right)
        {
            return !(left == right);
        }

        public static bool operator ==(JetId left, JetId right)
        {
            return Equals(left?.Id, right?.Id);
        }
    }
}
