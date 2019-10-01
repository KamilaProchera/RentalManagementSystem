using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RentalSystem.Persistence;
using RentalSystem.Persistence.Models;

namespace RentalSystem.Services.Services
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DataContext _context;
        public RentalRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Rental> GetRentals()
        {
            return _context.Rentals;
        }

        public Dictionary<string, int> GetTheBestClients()
        {
            var rentals = GetRentals();
            var theBestClients = rentals.GroupBy(x=>x.CustomerId).Select(grouping =>new {
                client=grouping.Key,
                rentals=grouping.Count()
            }).OrderByDescending(x=>x.rentals).Take(10);

            var theBestClientsDict = theBestClients.ToDictionary(x=>x.client, x=>x.rentals);

            return theBestClientsDict;
        }

        public Dictionary<string, int> ScootersTotalRentalTime()
        {
            var rentals = GetRentals();
            var scooterTotalRentalTime = rentals.GroupBy(x => x.ScooterId).Select(grouping=>new {
                scooter=grouping.Key,
                rentalTime=grouping.Sum(x=>x.TotalTimeInMinutes)

            });
            var scootersTotalRentalTimeDictionary = scooterTotalRentalTime.ToDictionary(x=>x.scooter,x=>x.rentalTime );

            return scootersTotalRentalTimeDictionary;
        }
    }
}
