using ReadModel.Common;

namespace ReadModel.Modules.Jet
{
    public record Jet : IReadEntity
    {
        public string Id { get; init; }

        public string Name { get; init; }
    }
}
