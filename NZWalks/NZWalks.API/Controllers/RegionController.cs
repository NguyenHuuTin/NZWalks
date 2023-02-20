using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using NZWalks.API.Models.DTO;
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
            var regionResult = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionResult);
        }

        [HttpGet]
        [Route("{Id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid Id)
        {
            var region = await regionResponsitory.GetAsync(Id);
            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            // Validate add regoin
            if (!ValidateAddRegoinAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }

            var region = new Models.Domain.Region()
            {
                code = addRegionRequest.code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            region = await regionResponsitory.AddAsync(region);

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                code = region.code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var region = await regionResponsitory.DeleteAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync(Guid id, [FromBody]Models.DTO.UpdateRegionDTO updateRegionDTO)
        {
            var region = new Models.Domain.Region()
            {
                code = updateRegionDTO.code,
                Area = updateRegionDTO.Area,
                Lat = updateRegionDTO.Lat,
                Long = updateRegionDTO.Long,
                Name = updateRegionDTO.Name,
                Population = updateRegionDTO.Population
            };

            region = await regionResponsitory.UpdateAsync(id, region);

            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);

        }

        private bool ValidateAddRegoinAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $"Add regoin data is required.");
                return false;
            }

            if (String.IsNullOrWhiteSpace(addRegionRequest.code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.code), $"{nameof(addRegionRequest.code)} can not be null or empty or white space.");
            }

            if(ModelState.ErrorCount > 0) return false;

            return true;
        }
    }
}
