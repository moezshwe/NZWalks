using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalkAPI.Data
{
    public class NZWalksAuthDbContext :IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleID = "378cd087-8253-4557-b4fb-e5862517a33a";
            var writerRoleID = "0571bdc8-8ee2-4ebc-b699-cd04d8bf2729";

            var roles = new List<IdentityRole> { 
                new IdentityRole
                {
                    Id= readerRoleID ,
                    ConcurrencyStamp=readerRoleID,
                    Name="Reader",
                    NormalizedName ="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id= writerRoleID ,
                    ConcurrencyStamp = writerRoleID,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper ()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
