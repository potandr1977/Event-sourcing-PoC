
using ReadModel.Flight;
using System.Linq.Expressions;

namespace Application.Services
{
    public interface IFlightReader
    {
        Task<Flight> GetByIdAsync(string id);

        Task<IEnumerable<Flight>> FindAllAsync(Expression<Func<Flight, bool>> predicate);

        Task<IEnumerable<Seat>> GetItemsOfAsync(string flightId);
    }
}
