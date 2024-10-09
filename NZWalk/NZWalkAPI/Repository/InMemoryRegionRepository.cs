using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
    public class InMemoryRegionRepository 
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>
                {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "dfdfdf",
                    Name = "dfdtetrer"
                }
            };
        }
    }
}
