using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;
using NZWalkAPI.Repository;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
           this.imageRepository = imageRepository;
        }

      

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requestDto)
        {
            ValidateFileUpload(requestDto);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = requestDto.File,
                    FileExtension = Path.GetExtension(requestDto.File.FileName),
                    FileSizeInBytes = requestDto.File.Length,
                    FileName = requestDto.FileName,
                    FileDescription = requestDto.FileDescription
                };

                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto requestDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(Path.GetExtension(requestDto.File .FileName )) )
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }
            if (requestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
