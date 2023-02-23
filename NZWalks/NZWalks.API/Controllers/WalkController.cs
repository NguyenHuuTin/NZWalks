using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Responsitories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller
    {
        private readonly IWalkReponsitory walkReponsitory;
        private readonly IMapper mapper;

        public WalkController(IWalkReponsitory walkReponsitory, IMapper mapper)
        {
            this.walkReponsitory = walkReponsitory;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "read")]
        public async Task<IActionResult> GetAllWalk()
        {
            var walks = await walkReponsitory.GetAllAsync();
            var walkResult = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walkResult);
        }

        [HttpGet]
        [Route("{Id:guid}")]
        [ActionName("GetWalkAsync")]
        [Authorize(Roles = "read")]
        public async Task<IActionResult> GetWalkAsync(Guid Id)
        {
            var walk = await walkReponsitory.GetAsync(Id);
            if (walk == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(regionDTO);
        }

        [HttpPost]
        [Authorize(Roles = "write")]
        public async Task<IActionResult> AddWalkAsync(AddWalkRequest addWalkRequest)
        {
            // Validate add regoin
            /*if (!ValidateAddRegoinAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }*/

            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
                RegionId = addWalkRequest.RegionId
            };

            walk = await walkReponsitory.AddAsync(walk);

            var walkDTO = new Models.DTO.Walk()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "write")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var walk = await walkReponsitory.DeleteAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "write")]
        public async Task<IActionResult> UpdateRegionAsync(Guid id, [FromBody] Models.DTO.UpdateWalkDTO updateWalkDTO)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = updateWalkDTO.Name,
                Length = updateWalkDTO.Length,
                RegionId = updateWalkDTO.RegionId,
                WalkDifficultyId = updateWalkDTO.WalkDifficultyId
            };

            walk = await walkReponsitory.UpdateAsync(id, walk);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walkDTO);

        }
    }
}
