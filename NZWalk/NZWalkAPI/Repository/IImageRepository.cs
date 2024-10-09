using NZWalkAPI.Models.Domain;
using System.Net;

namespace NZWalkAPI.Repository
{
    public interface IImageRepository
    {
       Task<Image> Upload(Image Image);
    }
}
