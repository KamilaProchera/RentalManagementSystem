using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalSystem.Persistence.Models
{
    public class Scooter
    {
        [Key]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public bool Available { get; set; } = false;
        public bool Damaged { get; set; } = false;
    }
}
