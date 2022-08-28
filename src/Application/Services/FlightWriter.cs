using Domain;
using Domain.Modules.FlightModule;
using Domain.Persistence;

namespace Application.Services
{
    public class FlightWriter : IFlightWriter
    {
        private readonly IRepository<Flight, FlightId> flightRepository;

        public async Task AddTicketAsync(string flightId, string ticketId, int quantity)
        {
            var flight = await flightRepository.GetByIdAsync(new FlightId(flightId));

            flight.AddTicket(new Domain.TicketModule.TicketId(ticketId), quantity);
        }

        public Task ChangeTicketQuantityAsync(string flightId, string ticketId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(string jetId)
        {
            throw new NotImplementedException();
        }
    }
}
