using Domain;
using Domain.Modules.FlightModule;
using ReadModel.Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace EventSourcingCQRS.Application.Services
{
    public class FlightReader : IFlightReader
    {
        private readonly IReadOnlyRepository<Flight> flightRepository;
        private readonly IReadOnlyRepository<Seat> seatRepository;

        public FlightReader(IReadOnlyRepository<Flight> flightRepository, IReadOnlyRepository<Seat> seatRepository)
        {
            this.flightRepository = flightRepository;
            this.seatRepository = seatRepository;
        }

        public async Task<IEnumerable<Flight>> FindAllAsync(Expression<Func<Flight, bool>> predicate)
        {
            return await flightRepository.FindAllAsync(predicate);
        }

        public async Task<Flight> GetByIdAsync(string id)
        {
            return await flightRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Seat>> GetItemsOfAsync(string cartId)
        {
            return await seatRepository.FindAllAsync(x => x.fli == cartId);
        }
    }
}
