using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk newWalk)
        {
            
        }
        

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks.Include(x => x.region).ToListAsync();
        }

    }
}
