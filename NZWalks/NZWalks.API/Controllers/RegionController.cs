using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Domain.Models;
using NZWalks.API.Responsitories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionResponsitory regionResponsitory;
        private readonly IMapper mapper;

        public RegionController(IRegionResponsitory regionResponsitory, IMapper mapper)
        {
            this.regionResponsitory = regionResponsitory;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegion() 
        {
            var regions = await regionResponsitory.GetAllAsync();
            var regionResult = mapper.Map<List<Domain.DTO.Region>>(regions);
            return Ok(regionResult);
        }
    }
}
