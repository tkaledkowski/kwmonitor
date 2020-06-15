using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoronaWirusMonitor3.Models;

namespace KWMonitor.Services
{
    public interface ICityService
    {
        Task<List<City>> GetAll();
        City GetById(int id);
        Task<bool> Update(City city);

    }
}
