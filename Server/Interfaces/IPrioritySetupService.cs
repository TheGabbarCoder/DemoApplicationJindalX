using Server.Models;

namespace Server.Interfaces
{
    public interface IPrioritySetupService
    {
        Task<IEnumerable<PrioritySetup>> GetAllAsync(string? search = null);
        Task<PrioritySetup> AddAsync(PrioritySetup setup);
        Task<bool> ToggleActive(int id);
        Task<byte[]> ExportCsv(IEnumerable<int>? selectedIds = null);
    }
}
