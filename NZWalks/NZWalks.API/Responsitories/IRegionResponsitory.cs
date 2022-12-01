using NZWalks.API.Domain.Models;

namespace NZWalks.API.Responsitories
{
    public interface IRegionResponsitory
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
