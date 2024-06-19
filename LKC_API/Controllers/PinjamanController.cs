using LKC_API.Models;
using LKC_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LKC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PinjamanController : ControllerBase
    {
        private readonly LKCService _lkcService;

        public PinjamanController(LKCService lkcService) =>
            _lkcService = lkcService;

        [HttpGet]
        public async Task<List<Pinjaman>> Get() =>
            await _lkcService.GetPinjamanAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Pinjaman>> Get(string id)
        {
            var pinjaman = await _lkcService.GetPinjamanAsync(id);

            if (pinjaman is null)
            {
                return NotFound();
            }

            return pinjaman;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pinjaman newPinjaman)
        {
            await _lkcService.CreatePinjamanAsync(newPinjaman);

            return CreatedAtAction(nameof(Get), new { id = newPinjaman.Id }, newPinjaman);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Pinjaman updatedPinjaman)
        {
            var pinjaman = await _lkcService.GetPinjamanAsync(id);

            if (pinjaman is null)
            {
                return NotFound();
            }

            updatedPinjaman.Id = pinjaman.Id;

            await _lkcService.UpdatePinjamanAsync(id, updatedPinjaman);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pinjaman = await _lkcService.GetPinjamanAsync(id);

            if (pinjaman is null)
            {
                return NotFound();
            }

            await _lkcService.RemovePinjamanAsync(id);

            return NoContent();
        }



    }
}
