

using NZWalks.API.Models.Domain;

namespace NZWalks.API.Responsitories
{
    public interface IWalkReponsitory
    {
        Task<IEnumerable<Walk>> GetAllAsync();

        Task<Walk> GetAsync(Guid Id);

        Task<Walk> AddAsync(Walk walk);

        Task<Walk> DeleteAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);
    }
   
}
