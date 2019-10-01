using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Api.Dtos;
using RentalSystem.Services.Services;

namespace RentalSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;
        public RentsController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;

        }
        // GET: api/Rents/GetTheBest10Clients
        [HttpGet]
        public IActionResult GetTheBestClients()
        {

            var theBestCustomers = _rentalRepository.GetTheBestClients();


            if(theBestCustomers==null)
            {
                ModelState.AddModelError("", $"The list of customers is empty");
                return StatusCode(500, ModelState);
            }

            var customersDto = new List<CustomerDto>();
            foreach(var customer in theBestCustomers)
            {
                customersDto.Add(new CustomerDto {
                   CustomerId=customer.Key,
                   NumberOfRents=customer.Value
                });
            }
            return Ok(customersDto);
        }

        // GET: api/Rents/ScootersTotalRentalTime
        [HttpGet]
        public IActionResult ScootersTotalRentalTime()
        {

            var scootersRentalTime = _rentalRepository.ScootersTotalRentalTime();
            if(scootersRentalTime==null)
            {
                ModelState.AddModelError("", $"The list of scooters is empty");
                return StatusCode(500, ModelState);

            }
            var scootersTotalRentalTime = new List<ScooterRentalTimeDto>();
            foreach (var scooter in scootersRentalTime)
            {
                scootersTotalRentalTime.Add(new ScooterRentalTimeDto {
                    ScooterId=scooter.Key,
                    TotalRentalTimeInMinutes=scooter.Value
                });

            }
            return Ok(scootersTotalRentalTime);
        }
    }
}
