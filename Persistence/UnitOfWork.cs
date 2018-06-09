using System.Threading.Tasks;

namespace vega.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public VegaDbContext Context { get; }
        public UnitOfWork(VegaDbContext context)
        {
            Context = context;
        }

        public async Task CompleteAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}