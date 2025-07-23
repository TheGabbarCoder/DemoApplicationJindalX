using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrioritySetupController: ControllerBase
    {
        private readonly IPrioritySetupService _service;

        public PrioritySetupController(IPrioritySetupService service)
        {
            _service = service;
        }      

        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PrioritySetup setup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.AddAsync(setup);
            return Ok(created);
        }

       
        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var success = await _service.ToggleActive(id);
            if (!success) return NotFound();
            return NoContent();
        }

        
        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportCsv([FromQuery] List<int>? ids = null)
        {
            var csvBytes = await _service.ExportCsv(ids);
            return File(csvBytes, "text/csv", "priority-setup-export.csv");
        }

        // For Mock test only.....
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = null)
        {
            var setups = await _service.GetAllAsync(search);
            return Ok(setups);
        }
    }

}
