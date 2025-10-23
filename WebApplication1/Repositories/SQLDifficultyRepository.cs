using Microsoft.EntityFrameworkCore;
using NZWalks.Repositories.Contracts;
using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLDifficultyRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            await _dbContext.Difficulties.AddAsync(difficulty);
            await _dbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(Guid id)
        {
            var difficultyDomain = await _dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if(difficultyDomain == null)
            {
                return null;
            }
            _dbContext.Difficulties.Remove(difficultyDomain);
            await _dbContext.SaveChangesAsync();
            return difficultyDomain;
        }

        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await _dbContext.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetByIdAsync(Guid id)
        {
            var difficultyDomain = await _dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (difficultyDomain == null)
            {
                return null;
            }
            return difficultyDomain;

        }

        public async Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty)
        {
            var difficultyDomain = await _dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (difficultyDomain == null)
            {
                return null;
            }
            difficultyDomain.Name = difficulty.Name;
            await _dbContext.SaveChangesAsync();
            return difficultyDomain;
        }
    }
}
