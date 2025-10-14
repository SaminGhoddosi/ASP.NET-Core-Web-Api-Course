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
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = newWalk.Name;
            existingWalk.Description = newWalk.Description;
            existingWalk.LengthInKm = newWalk.LengthInKm;
            existingWalk.WalkImageUrl = newWalk.WalkImageUrl;
            existingWalk.DifficultyId = newWalk.DifficultyId;
            existingWalk.RegionId = newWalk.RegionId;
            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }


        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize)
        {                                           // se não me disser o filterOn, assumo que é nulo
            //sem await porque ainda não vai para o banco
            var walks = _dbContext.Walks.Include(x => x.difficulty).Include(x => x.region).AsQueryable();  //sem ele, esse walks iria para o banco, mas com o queryable, vai só no final
            if(walks == null)
            {
                return null;
            }
            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals(nameof(Walk.Name), StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks
                        .Where(x => x.Name.Contains(filterQuery));
                }
            }
            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals(nameof(Walk.Name), StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals(nameof(Walk.LengthInKm), StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include(x => x.region).Include(x => x.difficulty).FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomain = await _dbContext.Walks.FirstOrDefaultAsync(x => x.id == id);
            if (walkDomain == null)
            {
                return null;
            }
            _dbContext.Walks.Remove(walkDomain);
            await _dbContext.SaveChangesAsync();
            return walkDomain;

        }

        public async Task<List<Walk>> GetByNameAsync(string name, string? sortBy, bool isAscending, int pageSize, int pageNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return await _dbContext.Walks.ToListAsync(); //retornar tudo, já que o nome não veio
            }
            var walks = _dbContext.Walks.Include(x => x.region).Include(x => x.difficulty).Where(x => x.Name.Contains(name));
            
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals(nameof(Walk.Name))){
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
            }
            int skip = (pageNumber - 1) * pageSize;
            return await walks.Skip(skip).Take(pageSize).ToListAsync(); 
        }
    }
}
