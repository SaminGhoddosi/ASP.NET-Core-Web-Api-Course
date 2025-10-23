using WebApplication1.Models.Domain;

namespace NZWalks.Repositories.Contracts
{
    public interface IDifficultyRepository
    {
        public Task<List<Difficulty>> GetAllAsync();
        public Task<Difficulty?> GetByIdAsync(Guid id);
        public Task<Difficulty> CreateAsync(Difficulty difficulty);
        public Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty);
        public Task<Difficulty?> DeleteAsync(Guid id);
    }
}
