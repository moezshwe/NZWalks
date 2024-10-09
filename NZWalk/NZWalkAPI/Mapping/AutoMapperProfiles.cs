using AutoMapper;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;

namespace NZWalkAPI.Mapping
{
    public class AutoMapperProfiles :Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk,WalkDto >().ReverseMap();
            CreateMap<Difficulty ,DifficultyDto>().ReverseMap();    
            CreateMap<UpdateWalkRequestDto ,Walk> ().ReverseMap();  
        }
    }
}
