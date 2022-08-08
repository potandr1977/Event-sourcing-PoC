using Domain;
using Domain.Modules.FlightModule;
using Domain.Modules.FlightModule.Events;
using Domain.Persistence;
using Domain.TicketModule;

namespace EventSourcingCQRS.Application.Services
{
    public class FlightWriter : IFlightWriter
    {
        private readonly IRepository<Flight, FlightId> flightRepository;

        public FlightWriter(IRepository<Flight, FlightId> flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public async Task ReserveTicketAsync(string flightId, string ticketId, int quantity)
        {
            var flight = await this.flightRepository.GetByIdAsync(new FlightId(flightId));
            mediator.Send(new TicketReservedEvent(ticketId, quantity));
            flight.AddTicket(new TicketId(ticketId), quantity);
            await flightRepository.SaveAsync(flight);
        }

        public async Task ChangeProductQuantityAsync(string cartId, string productId, int quantity)
        {
            var cart = await flightRepository.GetByIdAsync(new CartId(cartId));

            _mediator.Send();
            cart.ChangeProductQuantity(new ProductId(productId), quantity);
            await flightRepository.SaveAsync(cart);
        }

        public async Task CreateAsync(string customerId)
        {
            var cart = new Cart(CartId.NewCartId(), new CustomerId(customerId));

            subscriber.Subscribe<CartCreatedEvent>(async @event => await HandleAsync(cartCreatedEventHandlers, @event));
            await flightRepository.SaveAsync(cart);
        }

        public async Task HandleAsync<T>(IEnumerable<IDomainEventHandler<CartId, T>> handlers, T @event)
            where T : IDomainEvent<CartId>
        {
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event);
            }
        }
    }
}
