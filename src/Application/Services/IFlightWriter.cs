using System.Threading.Tasks;

namespace Application.Services
{
    public interface IFlightWriter
    {
        Task CreateAsync(string jetId);

        Task AddTicketAsync(string flightId, string ticketId, int quantity);

        Task ChangeTicketQuantityAsync(string flightId, string ticketId, int quantity);
    }
}
