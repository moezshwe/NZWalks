using Microsoft.EntityFrameworkCore;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Data
{
    public class NZWalkDbContext : DbContext
    {
        public NZWalkDbContext(DbContextOptions<NZWalkDbContext > dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var difficulties = new List<Difficulty>()
             {
                new Difficulty ()
             {
                 Id=Guid.Parse ("ec5b14bb-bbfb-4b6e-b08d-4daabefc1718"),
                 Name="Easy"
             },
                 new Difficulty ()
             {
                 Id=Guid.Parse ("b3fefa48-6d27-4742-850e-241104c345e0"),
                 Name="Medium"
             },
                  new Difficulty ()
             {
                 Id=Guid.Parse ("ce3341df-a57c-4eb1-8faa-f3dd6811ee03"),
                 Name="Hard"
             }

                };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>() {
                new Region () {
                    Id=Guid.Parse ("ca07af39-fb58-43bd-88c3-41f4f26e6927"),
                    Name="Auckland",
                    Code="AKL",
                    RegionImageUrl="someThing.jpg"
                } ,
                       new Region()
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                }
            };
            modelBuilder.Entity <Region>().HasData(regions);
        }
    }
}
