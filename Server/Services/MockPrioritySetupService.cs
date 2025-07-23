using Server.Interfaces;
using Server.Models;

namespace Server.Services
{
    public class MockPrioritySetupService : IPrioritySetupService
    {
        private static List<PrioritySetup> _mockSetups = new()
        {
            new PrioritySetup
            {
                Id = 1,
                PayerId = 1,
                LocationId = 1,
                AgeingBucketId = 1,
                PriorityTypeId = 1,
                TotalBalance = 1450,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new PrioritySetup
            {
                Id = 2,
                PayerId = 2,
                LocationId = 2,
                AgeingBucketId = 2,
                PriorityTypeId = 2,
                TotalBalance = 1790,
                IsActive = false,
                CreatedAt = DateTime.UtcNow
            }
        };

        public Task<IEnumerable<PrioritySetup>> GetAllAsync(string? search = null)
        {
            IEnumerable<PrioritySetup> results = _mockSetups;

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                results = results.Where(s =>
                    s.TotalBalance.ToString().Contains(search) ||
                    s.PayerId.ToString() == search ||
                    s.LocationId.ToString() == search ||
                    s.AgeingBucketId.ToString() == search ||
                    s.PriorityTypeId.ToString() == search
                );
            }

            return Task.FromResult(results);
        }

        public Task<PrioritySetup> AddAsync(PrioritySetup setup)
        {
            setup.Id = _mockSetups.Count + 1;
            setup.IsActive = true;
            setup.CreatedAt = DateTime.UtcNow;
            _mockSetups.Add(setup);
            return Task.FromResult(setup);
        }

        public Task<bool> ToggleActive(int id)
        {
            var item = _mockSetups.FirstOrDefault(s => s.Id == id);
            if (item == null) return Task.FromResult(false);

            item.IsActive = !item.IsActive;
            return Task.FromResult(true);
        }

        public Task<byte[]> ExportCsv(IEnumerable<int>? selectedIds = null)
        {
            var query = selectedIds == null || !selectedIds.Any()
                ? _mockSetups
                : _mockSetups.Where(x => selectedIds.Contains(x.Id));

            var lines = new List<string> {
                "Id,PayerId,LocationId,AgeingBucketId,PriorityTypeId,TotalBalance,IsActive"
            };

            foreach (var s in query)
            {
                lines.Add($"{s.Id},{s.PayerId},{s.LocationId},{s.AgeingBucketId},{s.PriorityTypeId},{s.TotalBalance},{s.IsActive}");
            }

            var csv = string.Join("\n", lines);
            return Task.FromResult(System.Text.Encoding.UTF8.GetBytes(csv));
        }
    }
}
