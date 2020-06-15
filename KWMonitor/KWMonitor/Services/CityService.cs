using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoronaWirusMonitor3.Models;
using KoronaWirusMonitor3.Repository;
using Microsoft.EntityFrameworkCore;

namespace KWMonitor.Services
{
    public class CityService : ICityService
    {
        private readonly KWMContext _context;

        public CityService(KWMContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetAll()
        {
            return await _context.Cities.Include(c => c.District).ToListAsync();
        }

        public City GetById(int id)
        {
            return _context.Cities.Include(r => r.District).FirstOrDefault(r=>r.Id==id);
        }

        public async Task<bool> Update(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(city.Id))
                    return false;
                throw;
            }

            return true;
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }

}
