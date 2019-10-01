using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalSystem.Persistence.Models
{
     public class RentalHistory
    {
        [Key]
        public int Id { get; set; }
        public Scooter Scooter { get; set; }
        public Customer Customer { get; set; }
        public DateTime RentalStart { get; set; }
        public DateTime? RentalFinish { get; set; }
        public TimeSpan? TotalTime { get; set; }
    }
}
