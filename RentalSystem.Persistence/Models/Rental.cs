using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalSystem.Persistence.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        public string RentalId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ScooterId { get; set; }
        public string ScooterMake { get; set; }
        public DateTime RentalStart { get; set; }
        public DateTime? RentalFinish { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public int TotalTimeInMinutes { get; set; }
    }
}
