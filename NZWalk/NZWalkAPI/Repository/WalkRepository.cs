using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalkAPI.Repository
{
    public class WalkRepository : IWalkRepository

    {
        private readonly NZWalkDbContext dbContext;

        public WalkRepository(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAnsyc(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsyn(Guid id)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk != null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllasync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInkm) : walks.OrderByDescending(x => x.LengthInkm);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults ).Take(pageSize ).ToListAsync();
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

        }

        public async Task<Walk?> GetByID(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> Updateasyn(Guid id, Walk walk)
        {
            var exisitnwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitnwalk != null)
            {
                return null;
            }
            exisitnwalk.Name = walk.Name;
            exisitnwalk.Description = walk.Description;
            exisitnwalk.LengthInkm = walk.LengthInkm;
            exisitnwalk.WalkImageUrl = walk.WalkImageUrl;
            exisitnwalk.DifficultyID = walk.DifficultyID;
            exisitnwalk.RegionID = walk.RegionID;
            await dbContext.SaveChangesAsync();
            return exisitnwalk;
        }


    }
}
