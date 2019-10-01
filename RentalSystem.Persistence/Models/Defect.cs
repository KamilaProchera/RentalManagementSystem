using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalSystem.Persistence.Models
{
    public class Defect
    {
        [Key]
        public int Id { get; set; }
        public Scooter scooter { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool Fixed { get; set; } = false;

    }
}
