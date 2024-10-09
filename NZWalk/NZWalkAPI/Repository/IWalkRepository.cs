using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAnsyc(Walk walk);
        Task<List<Walk>> GetAllasync(string? filterON = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByID(Guid id);
        Task<Walk?> Updateasyn(Guid id, Walk walk);
        Task<Walk?> DeleteAsyn(Guid id);
    }
}
