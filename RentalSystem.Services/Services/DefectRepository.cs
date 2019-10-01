using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RentalSystem.Persistence;
using RentalSystem.Persistence.Models;

namespace RentalSystem.Services.Services
{

    public class DefectRepository : IDefectRepository
    {
        private readonly DataContext _context;
        public DefectRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddDefect(int scooterId, Defect defect)
        {
            var reportTime = DateTime.UtcNow;
            var scooter = _context.Scooters.FirstOrDefault(x=>x.Id==scooterId);
            var rentalHistory = GetTheLatestRental(scooterId);

            if(rentalHistory!=null&&rentalHistory.RentalFinish==null)
            {
                CreateDefectItem(scooter,defect,reportTime);
                return true;
            }

            var finishTime = (DateTime)rentalHistory.RentalFinish;
            var gapTime = reportTime - finishTime;
            if(rentalHistory!=null&&(int)gapTime.TotalMinutes<15)
            {
                CreateDefectItem(scooter,defect,reportTime);
                return true;
            }
            return false;
        }

        public Defect CreateDefectItem(Scooter scooter, Defect defect, DateTime time)
        {
           
            var newDefect = new Defect
            {
                scooter = scooter,
                Title = defect.Title,
                Desc=defect.Desc,
                Created = time,

            };
            scooter.Available = false;
            scooter.Damaged = true;
            _context.Update(scooter);
            _context.Add(newDefect);
            _context.SaveChanges();
            return newDefect;
            
        }

        public Defect GetDefectByScooterId(int scooterId)
        {
            return _context.Defects.Where(x => x.scooter.Id == scooterId).OrderByDescending(x=>x.Created).FirstOrDefault();
        }

        public bool MarkFixed(int scooterId)
        {
            var defect = GetDefectByScooterId(scooterId);
            if (defect != null)
            {

                defect.Fixed = true;

                var scooter = _context.Scooters.FirstOrDefault(x => x.Id == scooterId);
                scooter.Available = true;
                scooter.Damaged = false;
                _context.Update(scooter);
                _context.Update(defect);
                _context.SaveChanges();
                return true;
            }
            return false;

        }
        public RentalHistory GetTheLatestRental(int scooterId)
        {
            return _context.RentalHistory.Where(x => x.Scooter.Id == scooterId).OrderByDescending(x => x.RentalStart).FirstOrDefault();
        }
       
    }
}
