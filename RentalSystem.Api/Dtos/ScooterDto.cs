using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalSystem.Api.Dtos
{
    public class ScooterDto
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public bool Available { get; set; }
        public bool Damaged { get; set; }
    }
}
