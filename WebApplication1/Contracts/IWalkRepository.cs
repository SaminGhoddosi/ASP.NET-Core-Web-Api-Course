using WebApplication1.Models.Domain;

namespace WebApplication1.Contracts
{
    public interface IWalkRepository
    {
        public Task<Walk> CreateAsync(Walk walk);
        public Task<Walk> UpdateAsync(Guid id, Walk walk);
        public Task<List<Walk>> GetAllAsync();
    }
}
