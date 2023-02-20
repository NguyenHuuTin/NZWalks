using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllWalk()
        {
            var walks = await walkReponsitory.GetAllAsync();
            var walkResult = mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walkResult);
        }
    }
}
