﻿using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Responsitories
{
    public class RegionResponsitory : IRegionResponsitory
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public RegionResponsitory(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async  Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region); 
            await nZWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid Id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }

            existingRegion.code = region.code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Long = region.Long;
            existingRegion.Lat = region.Lat;
            existingRegion.Population = region.Population;

            nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
