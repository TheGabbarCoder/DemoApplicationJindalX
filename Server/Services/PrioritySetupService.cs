using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace Server.Services
{
    public class PrioritySetupService: IPrioritySetupService
    {
        private readonly AppDbContext _context;

        public PrioritySetupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PrioritySetup>> GetAllAsync(string? search = null)
        {
            var query = _context.PrioritySetups
                .Include(p => p.Payer)
                .Include(p => p.Location)
                .Include(p => p.AgeingBucket)
                .Include(p => p.PriorityType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.Payer.Name.Contains(search) ||
                    p.Location.Name.Contains(search) ||
                    p.AgeingBucket.Name.Contains(search) ||
                    p.PriorityType.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<PrioritySetup> AddAsync(PrioritySetup setup)
        {
            setup.IsActive = true;
            setup.CreatedAt = DateTime.UtcNow;

            _context.PrioritySetups.Add(setup);
            await _context.SaveChangesAsync();

            return setup;
        }

        public async Task<bool> ToggleActive(int id)
        {
            var item = await _context.PrioritySetups.FindAsync(id);
            if (item == null) return false;

            item.IsActive = !item.IsActive;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<byte[]> ExportCsv(IEnumerable<int>? selectedIds = null)
        {
            var query = _context.PrioritySetups
                .Include(p => p.Payer)
                .Include(p => p.Location)
                .Include(p => p.AgeingBucket)
                .Include(p => p.PriorityType)
                .AsQueryable();

            if (selectedIds != null && selectedIds.Any())
                query = query.Where(p => selectedIds.Contains(p.Id));

            var data = await query.ToListAsync();

            using var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data.Select(item => new
                {
                    Payer = item.Payer.Name,
                    Location = item.Location.Name,
                    AgeingBucket = item.AgeingBucket.Name,
                    Priority = item.PriorityType.Name,
                    TotalBalance = item.TotalBalance,
                    IsActive = item.IsActive
                }));
                writer.Flush();
            }

            return memoryStream.ToArray();
        }
    }
}
