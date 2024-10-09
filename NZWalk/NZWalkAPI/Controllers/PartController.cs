using Microsoft.AspNetCore.Mvc;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : Controller
    {
        [HttpGet]
        public IActionResult GetPart()
        {
            List<Part> parts = new List<Part>
{
    new Part { Id = 1, Description = "Engine", Price = 2500.00M, Quantity = 10 },
    new Part { Id = 2, Description = "Gearbox", Price = 1500.50M, Quantity = 5 },
    new Part { Id = 3, Description = "Wheel", Price = 100.75M, Quantity = 50 }
};
            return Ok(parts);

        }
    }

    public class Part
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
