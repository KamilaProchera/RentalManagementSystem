using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RentalSystem.Persistence;
using RentalSystem.Persistence.Models;

namespace RentalSystem.Services.Services
{
    public class ScooterRepository : IScooterRepository
    {
        private readonly DataContext _context;
        public ScooterRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Scooter> GetAvailableScooters()
        {
            return _context.Scooters.Where(x=>x.Available==true);
        }

        public RentalHistory GetTheLatestRental(int scooterId)
        {
            return _context.RentalHistory.Where(x => x.Scooter.Id==scooterId).OrderByDescending(x=>x.RentalStart).FirstOrDefault();
        }
        public Rental CreateRentalItem(Scooter scooter, Customer customer, RentalHistory rentalHistory)
        {
            var rentalItem = new Rental
            {
                RentalId=rentalHistory.Id.ToString(),
                CustomerId=customer.Id.ToString(),
                CustomerName=customer.Name,
                ScooterId=scooter.Id.ToString(),
                ScooterMake=scooter.Make,
                RentalStart=rentalHistory.RentalStart,
                RentalFinish=rentalHistory.RentalFinish,
                TotalTime=rentalHistory.TotalTime

            };
            _context.Add(rentalItem);
            _context.SaveChanges();
            return rentalItem;
        }
        public bool RentScooter(int scooterId, int customerId)
        {
            var now = DateTime.UtcNow;
            var scooter = _context.Scooters.FirstOrDefault(x=>x.Id==scooterId);
            if(scooter==null||customerId==null)
            {
                return false;
            }
            if(!scooter.Available||scooter.Damaged)
            {
                return false;
            }
            var customer = _context.Customers.FirstOrDefault(x=>x.Id==customerId);
            var rentalHistoryItem = new RentalHistory
            {
                Customer = customer,
                Scooter = scooter,
                RentalStart = now
            };
            scooter.Available = false;
            _context.Update(scooter);
            _context.Add(rentalHistoryItem);
            _context.SaveChanges();

            CreateRentalItem(scooter,customer,rentalHistoryItem);
            return true;
        }

        public bool ReturnScooter(int scooterId, int customerId)
        {
            var now = DateTime.Now;
            var historyItem = _context.RentalHistory.FirstOrDefault(x => x.Scooter.Id == scooterId && x.Customer.Id == customerId && x.RentalFinish == null);
            var rental = _context.Rentals.FirstOrDefault(x=>x.ScooterId==scooterId.ToString()&&x.CustomerId==customerId.ToString()&&x.RentalFinish==null);
            var scooter = _context.Scooters.FirstOrDefault(x=>x.Id==scooterId);
            if(historyItem==null)
            {
                return false;
            }

            // Update rentalhistory details
            historyItem.RentalFinish = now;
            var historyItemStart = historyItem.RentalStart;
            var historyItemFinish = historyItem.RentalFinish;
            historyItem.TotalTime = historyItemFinish - historyItemStart;
            _context.Update(historyItem);
            _context.SaveChanges();

            //Update scooter details

            scooter.Available = true;
            _context.Update(scooter);

            //Update rental details
            rental.RentalFinish = now;
            rental.TotalTime = historyItem.TotalTime;
            rental.TotalTimeInMinutes = (int)rental.TotalTime.Value.TotalMinutes;
            _context.Update(rental);
            _context.SaveChanges();
            
            return true;

        }
    }
}
