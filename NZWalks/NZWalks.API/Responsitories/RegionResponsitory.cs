using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Domain.Models;

namespace NZWalks.API.Responsitories
{
    public class RegionResponsitory : IRegionResponsitory
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public RegionResponsitory(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }
    }
}
