using Domain;
using Domain.Modules.FlightModule;
using Domain.Modules.FlightModule.Events;
using Domain.Persistence;
using Domain.TicketModule;

namespace Application.Services
{
    public class FlightWriter : IFlightWriter
    {
        public Task AddTicketAsync(string flightId, string ticketId, int quantity)
        {
            throw new NotImplementedException();
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
