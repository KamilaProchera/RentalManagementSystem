using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalSystem.Api.Dtos;
using RentalSystem.Persistence.Models;
using RentalSystem.Services.Services;

namespace RentalSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScootersController : ControllerBase
    {
        private readonly IScooterRepository _scooterRepository;
        private readonly IDefectRepository _defectRepository;
        public ScootersController(IScooterRepository scooterRepository, IDefectRepository defectRepository)
        {
            _scooterRepository = scooterRepository;
            _defectRepository = defectRepository;
        }

        // GET: api/Scooters/RentScooter/1
        //take id of customer from body
        [HttpPost("{id}")]
        public IActionResult RentScooter(int id, [FromBody] Customer customer)
        {
            var rented = _scooterRepository.RentScooter(id, customer.Id);
            if (!rented)
            {
                ModelState.AddModelError("", $"Scooter is not available or the Id you entered is wrong");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        // POST: api/Scooters/ReturnScooter/1
        //take id of customer from body
        [HttpPost("{id}")]
        public IActionResult ReturnScooter(int id,[FromBody] Customer customer)
        {
            var returned = _scooterRepository.ReturnScooter(id,customer.Id);
            if(!returned)
            {
                ModelState.AddModelError("", $"You are not entitled to return the scooter or the Id you entered is wrong");
                return StatusCode(500,ModelState);
            }
            return Ok();
        }
        // POST: api/Scooters/MarkScooterAsFixed/1
        [HttpPost("{id}")]
        public IActionResult MarkScooterAsFixed(int id)
        {
            var returned = _defectRepository.MarkFixed(id);
            if (!returned)
            {
                ModelState.AddModelError("", $"Can't find the scooter in database");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        // GET: api/Scooters/GetAvailableScooters
        [HttpGet]
        public IActionResult GetAvailableScooters()
        {
            var availableScooters = _scooterRepository.GetAvailableScooters();
            var scootersDto = new List<ScooterDto>();
            foreach (var scooter in availableScooters)
            {
                scootersDto.Add(new ScooterDto
                {
                    Id=scooter.Id,
                    Make=scooter.Make,
                    Model=scooter.Model,
                    Available=scooter.Available,
                    Damaged=scooter.Damaged

                });
            }
            return Ok(scootersDto);
        }

        // POST: api/Scooters/AddDefect/1
        [HttpPost("{id}")]
        public IActionResult AddDefect(int id,[FromBody] Defect defect)
        {
            var addedDefect = _defectRepository.AddDefect(id,defect);
            if (!addedDefect)
            {
                ModelState.AddModelError("", $"You can not report a defect, It's too late");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

      
    }
}
