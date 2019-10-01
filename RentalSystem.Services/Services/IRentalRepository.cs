using RentalSystem.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalSystem.Services.Services
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> GetRentals();
        Dictionary<string, int> ScootersTotalRentalTime();
        Dictionary<string,int> GetTheBestClients();
    }
}
