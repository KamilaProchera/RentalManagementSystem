using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalSystem.Api.Dtos
{
    public class CustomerDto
    {
        public string CustomerId { get; set; }
        public int NumberOfRents { get; set; }
    }
}
