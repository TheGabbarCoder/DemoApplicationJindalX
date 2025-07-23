using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/master")]
    public class MasterDataController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly bool _useMockData = true; // Set it to 'false' for real database and true for mock data, also un-Comment the mock-service from Program.cs

        public MasterDataController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("payers")]
        public async Task<IActionResult> GetPayers()
        {
            if (_useMockData)
            {
                var payers = new List<Payers>
                {
                    new() { Id = 1, Name = "Payer A" },
                    new() { Id = 2, Name = "Payer B" }
                };
                return Ok(payers);
            }
            var data = await _context.Payers.ToListAsync();
            return Ok(data);
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations()
        {
            if (_useMockData)
            {
                var locations = new List<Location>
                {
                    new() { Id = 1, Name = "Location X" },
                    new() { Id = 2, Name = "Location Y" }
                };
                return Ok(locations);
            }
            var data = await _context.Locations.ToListAsync();
            return Ok(data);
        }

        [HttpGet("ageing-buckets")]
        public async Task<IActionResult> GetAgeingBuckets()
        {
            if (_useMockData)
            {
                var buckets = new List<AgeingBucket>
                {
                    new() { Id = 1, Name = "0-30 Days" },
                    new() { Id = 2, Name = "31-60 Days" }
                };
                return Ok(buckets);
            }
            var data = await _context.AgeingBuckets.ToListAsync();
            return Ok(data);
        }

        [HttpGet("priority-types")]
        public async Task<IActionResult> GetPriorityTypes()
        {
            if (_useMockData)
            {
                var priorities = new List<PriorityType>
                {
                    new() { Id = 1, Name = "High" },
                    new() { Id = 2, Name = "Medium" },
                    new() { Id = 3, Name = "Low" }
                };
                return Ok(priorities);
            }
            var data = await _context.PriorityTypes.ToListAsync();
            return Ok(data);
        }
    }
}
