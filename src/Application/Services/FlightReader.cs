
using ReadModel.Flight;
using ReadModel.Persistence;
using System.Linq.Expressions;

namespace Application.Services
{
    public class FlightReader : IFlightReader
    {
        public Task<IEnumerable<Flight>> FindAllAsync(Expression<Func<Flight, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Flight> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Seat>> GetBookedSeatsAmountAsync(string flightId)
        {
            throw new NotImplementedException();
        }
    }
}
