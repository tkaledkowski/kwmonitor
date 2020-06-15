using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoronaWirusMonitor3.Models;

namespace KWMonitor.Services
{
    public interface IDistrictService
    {
        Task<List<District>> GetAll();
        District GetById(int id);
        Task<bool> Update(District district);
    }
}
