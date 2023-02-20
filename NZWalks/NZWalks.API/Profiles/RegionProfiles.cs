using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionProfiles : Profile
    {
        public RegionProfiles()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>().ReverseMap();
        }
    }
}
