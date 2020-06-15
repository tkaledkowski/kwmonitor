using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoronaWirusMonitor3.Models;
using KoronaWirusMonitor3.Repository;
using KWMonitor.DTO;
using KWMonitor.Services;
using KWMonitor.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace KWMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly KWMContext _context;
        private readonly ICityService _cityServices;

        public CitiesController(KWMContext context, ICityService cityServices)
        {
            _context = context;
            _cityServices = cityServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _cityServices.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<City> GetCity(int id)
        {
            var validator = new IdValidator();
            var result = validator.Validate(id);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var city = _cityServices.GetById(id);
            if (city == null) return NotFound();
            return city;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id) return BadRequest();
            var validator = new CityValidator();
            var resultValid = validator.Validate(city);
            if (!resultValid.IsValid)
            {
                return BadRequest(resultValid.Errors);
            }

            var result = await _cityServices.Update(city);
            if (result)
            {
                return Ok();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<City>> PostCity(CityDto city)
        {
            var newCity = _context.Cities.Add(new City
            {
                Name = city.Name
            });
            await _context.SaveChangesAsync();
            var response = _context.Cities.Include(r => r.District).FirstOrDefault(r => r.Id == newCity.Entity.Id);
            return CreatedAtAction("GetCity", new {id = response.Id}, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null) return NotFound();
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return city;
        }
    }
    }
