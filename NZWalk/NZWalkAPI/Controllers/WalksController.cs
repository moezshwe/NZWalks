using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.CustomActionFilter;
using NZWalkAPI.Mapping;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;
using NZWalkAPI.Repository;
using System.Net;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase

    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }



        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomaimModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAnsyc(walkDomaimModel);

            return Ok(mapper.Map<WalkDto>(walkDomaimModel));

        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] string? filteringOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy,
           [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {


            var walksdomainmodel = await walkRepository.GetAllasync(filteringOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

         //   throw new Exception("This is a new excepiton");
            return Ok(mapper.Map<List<WalkDto>>(walksdomainmodel));

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByID(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<ActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.Updateasyn(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{id :Guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomainModel = await walkRepository.DeleteAsyn(id);
            if (deleteWalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));
        }
    }
}
