using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalSystem.Api.Dtos
{
    public class ScooterRentalTimeDto
    {
        public string ScooterId { get; set; }
        public int  TotalRentalTimeInMinutes { get; set; }
    }
}
