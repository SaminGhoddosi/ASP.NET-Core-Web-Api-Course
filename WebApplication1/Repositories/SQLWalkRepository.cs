using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.Data;
using WebApplication1.Mappings;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

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
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.id == id);
            if(existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = newWalk.Name;
            existingWalk.Description = newWalk.Description;
            existingWalk.LengthInKm = newWalk.LengthInKm;
            existingWalk.WalkImageUrl = newWalk.WalkImageUrl;
            existingWalk.region = newWalk.region;
            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }
        

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks.Include(x => x.region).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include(x => x.region).FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomain = await _dbContext.Walks.FirstOrDefaultAsync(x => x.id == id);
            if(walkDomain == null)
            {
                return null;
            }
            _dbContext.Walks.Remove(walkDomain);
            await _dbContext.SaveChangesAsync();
            return walkDomain;

        }
    }
}
