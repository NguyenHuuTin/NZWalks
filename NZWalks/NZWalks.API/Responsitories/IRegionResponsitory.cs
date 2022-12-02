using NZWalks.API.Domain.Models;

namespace NZWalks.API.Responsitories
{
    public interface IRegionResponsitory
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid Id);

        Task<Region> AddAsync(Region region);

        Task<Region> DeleteAsync(Guid id);

        Task<Region> UpdateAsync(Guid id, Region region);
    }
}
