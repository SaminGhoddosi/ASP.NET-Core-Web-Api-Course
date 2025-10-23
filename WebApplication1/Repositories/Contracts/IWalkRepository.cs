using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;

namespace NZWalks.Repositories.Contracts
{
    public interface IWalkRepository
    {
        public Task<Walk> CreateAsync(Walk walk);
        public Task<Walk> UpdateAsync(Guid id, Walk walk); //null is the default answer
        public Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending,
            int pageNumber, int pageSize);
        public Task<Walk?> GetByIdAsync(Guid id);
        //lista sempre retorna alguma coisa, então não precisa colocar null
        public Task<List<Walk>> GetByNameAsync(string name, string? sortBy, bool isAscending , int pageSize, int pageNumber);
        public Task<Walk?> DeleteAsync(Guid id);
    }
}
