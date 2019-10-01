using RentalSystem.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalSystem.Services.Services
{
    public interface IDefectRepository
    {
        bool AddDefect(int scooterId, Defect defect);
        Defect CreateDefectItem(Scooter scooter, Defect defect, DateTime time);
        Defect GetDefectByScooterId(int scooterId);
        bool MarkFixed(int scooterId);
        RentalHistory GetTheLatestRental(int scooterId);
    }
}
