using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionProfiles : Profile
    {
        public RegionProfiles()
        {
            CreateMap<Domain.Models.Region, Models.DTO.Region>().ReverseMap();
            CreateMap<Domain.Models.Walk, Models.DTO.Walk>().ReverseMap();
        }
    }
}
