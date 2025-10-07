using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Contracts
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync(); //sempre vai retornar lista, mesmo que vazia
        Task<Region?> GetByIdAsync(Guid id); //pode não encontrar nada
        Task<Region> CreateAsync(Region region); //vai receber um region, mesmo que vazio
        Task<Region?> UpdateAsync(Guid id, Region region); //pode não encontrar nada
        Task<Region?> DeleteAsync(Guid id); //pode não encontrar nada
    }
}
