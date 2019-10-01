using RentalSystem.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalSystem.Services.Services
{
    public interface IScooterRepository
    {
        bool RentScooter(int scooterId, int customerId);
        bool ReturnScooter(int scooterId, int customerId);
        RentalHistory GetTheLatestRental(int scooterId);
        IEnumerable<Scooter> GetAvailableScooters();
        Rental CreateRentalItem(Scooter scooter,Customer customer, RentalHistory rentalHistory);
        
    }
}
