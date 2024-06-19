using LKC_API.Models;
using LKC_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LKC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BukuController : ControllerBase
    {
        private readonly LKCService _lkcService;

        public BukuController(LKCService lkcService) =>
            _lkcService = lkcService;

        [HttpGet]
        //public async Task<List<Buku>> Get() =>
        //    await _lkcService.GetBukuAsync();
        public async Task<List<Buku>> Get([FromQuery] string txtsearch = null)
        {
            return await _lkcService.GetBukuAsync(txtsearch);
        }

    }
}
