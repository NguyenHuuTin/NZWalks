using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Domain.Models;

namespace NZWalks.API.Responsitories
{
    public class WalkReponsitory : IWalkReponsitory
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public WalkReponsitory(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            nZWalksDbContext.Walks.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await nZWalksDbContext.Walks.ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid Id)
        {
            return await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
            existingWalk.RegionId = walk.RegionId;

            nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
