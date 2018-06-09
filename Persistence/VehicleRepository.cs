using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence
{
    public class VehicleRepository: IVehicleRepository
    {
        public VegaDbContext Context { get; }
        public VehicleRepository(VegaDbContext context)
        {
            Context = context;
        }        
        public async Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true)
        {
            if(!includeRelated)
                return await Context.Vehicle.FindAsync(id);    

             return await Context.Vehicle
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.id == id);
        }

        public void Remove(Vehicle vehicle)
        {
            Context.Remove(vehicle);
        }
        public void Add(Vehicle vehicle)
        {
            Context.Add(vehicle);
        }
    }
}