using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoronaWirusMonitor3.Models;
using KoronaWirusMonitor3.Repository;
using KWMonitor.DTO;
using KWMonitor.Services;
using KWMonitor.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KWMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly KWMContext _context;
        private readonly IDistrictService _districtServices;

        public DistrictsController(KWMContext context, IDistrictService districtServices)
        {
            _context = context;
            _districtServices = districtServices;
        }

        // GET: api/Districts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistricts()
        {
            return await _districtServices.GetAll();
        }

        // GET: api/Districts/5
        [HttpGet("{id}")]
        public ActionResult<District> GetDistrict(int id)
        {
            var validator = new IdValidator();
            var result = validator.Validate(id);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var district = _districtServices.GetById(id);
            if (district == null) return NotFound();
            return district;
        }

        // PUT: api/Districts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrict(int id, District district)
        {
            if (id != district.Id) return BadRequest();
            var validator = new DistrictValidator();
            var resultValid = validator.Validate(district);
            if (!resultValid.IsValid)
            {
                return BadRequest(resultValid.Errors);
            }

            var result = await _districtServices.Update(district);
            if (result)
            {
                return Ok();
            }

            return NoContent();
        }
        // POST: api/Districts
        [HttpPost]
        public async Task<ActionResult<District>> PostDistrict(DistrictDto district)
        {
            var newDistrict = _context.Districts.Add(new District
            {
                Name = district.Name
            });
            await _context.SaveChangesAsync();
            var response = _context.Districts.Include(r => r.Region).FirstOrDefault(r=>r.Id == newDistrict.Entity.Id);
            return CreatedAtAction("GetDistrict", new {id = response.Id}, response);
        }

        //DELETE: api/Districts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<District>> DeleteDistrict(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null) return NotFound();
            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();
            return district;
        }
    }
}
