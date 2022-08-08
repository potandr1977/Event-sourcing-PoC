namespace Domain.Core
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T publishedEvent);
    }
}
