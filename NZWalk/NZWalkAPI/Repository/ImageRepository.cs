using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalkDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,NZWalkDbContext dbContext )
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image Image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",$"{ Image.FileName}{ Image.FileExtension}" );
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await Image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{Image.FileName}{Image.FileExtension}";
            Image.FilePath = urlFilePath;

            await dbContext.Images.AddAsync (Image);
            await dbContext.SaveChangesAsync();

            return Image;

        }
    }
}
