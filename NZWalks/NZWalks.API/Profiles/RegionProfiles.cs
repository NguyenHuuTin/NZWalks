using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionProfiles : Profile
    {
        public RegionProfiles()
        {
            CreateMap<Domain.Models.Region, Domain.DTO.Region>().ReverseMap();
        }
    }
}
