using System.Threading.Tasks;
using vega.Models;

namespace vega.Persistence
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true);
        void Remove(Vehicle vehicle);
        void Add(Vehicle vehicle);
    }
}