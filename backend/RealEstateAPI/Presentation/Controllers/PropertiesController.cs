namespace RealEstateAPI.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using RealEstateAPI.Application.Services;
    using RealEstateAPI.Domain.Entities;

    [ApiController]
    [Route("api/properties")]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _service;

        public PropertiesController(PropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var properties = await _service.GetPropertiesAsync(name, address, minPrice, maxPrice);
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(string id)
        {
            var property = await _service.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound();

            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] Property newProperty)
        {
            await _service.AddPropertyAsync(newProperty);
            return CreatedAtAction(nameof(GetProperty), new { id = newProperty.Id.ToString() }, newProperty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(string id, [FromBody] Property updatedProperty)
        {
            await _service.UpdatePropertyAsync(id, updatedProperty);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(string id)
        {
            await _service.DeletePropertyAsync(id);
            return NoContent();
        }
    }
}
