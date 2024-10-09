using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.CustomActionFilter;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;
using NZWalkAPI.Repository;
using System.Text.Json;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalkDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        //   [Authorize (Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
            //var regionsDomain = new List<Region>
            //{
            //    new Region{
            //        Id=Guid.NewGuid(),
            //        Name ="Auckland Region",
            //        Code="AKL",
            //        RegionImageUrl="https://www.bing.com/images/search?view=detailV2&ccid=GrH5anEA&id=B6D6985D252BA5FEDDF283A44893A927C8DD0017&thid=OIP.GrH5anEAE10XhX7Mzs9a2wHaE7&mediaurl=https%3A%2F%2Fa.cdn-hotels.com%2Fgdcs%2Fproduction169%2Fd1777%2Ff6e2ce38-5276-4429-a4e1-a79947606630.jpg&exph=1066&expw=1600&q=Auckland+Region+image+&simid=608052277753826371&FORM=IRPRST&ck=31C5B746FD6EE1126B7A8BC4745D0776&selectedIndex=0&itb=0&cw=1086&ch=512&ajaxhist=0&ajaxserp=0"
            //    },
            //       new Region{
            //        Id=Guid.NewGuid(),
            //        Name ="Wellington Region",
            //        Code="WLG",
            //        RegionImageUrl="https://www.bing.com/images/search?view=detailV2&ccid=y1T85HwM&id=BB49B7FBD67E5158389EC024AB08ABD2AFF4B616&thid=OIP.y1T85HwMNdb7mrab-duJHwHaE8&mediaurl=https%3A%2F%2Fwww.goodfreephotos.com%2Falbums%2Fnew-zealand%2Fwellington%2Fnight-skyline-on-the-waterfront-in-wellington-new-zealand.jpg&exph=2592&expw=3888&q=Wellington+Region+image+&simid=607995038720198248&FORM=IRPRST&ck=40840C76464202BD08528C7D8D9B9883&selectedIndex=0&itb=0&cw=1086&ch=512&ajaxhist=0&ajaxserp=0"
            //    }
            //};
            try
            {
               // throw new Exception("This is a custom exception");
                var regionsDomain = await regionRepository.GetAllAsync();

                logger.LogInformation($"Finished GetAllRegion request with data:{JsonSerializer.Serialize(regionsDomain)}");

                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }



            //var regionDto = new List<RegionDto>();
            //foreach (var region in regionsDomain)
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}

        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // var regionDomain = dbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);


            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
