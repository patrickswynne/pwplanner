using System.Collections.Generic;
using System.Threading.Tasks;

namespace WGUPlanner.Services
{
    public interface IDataTermStore<T>
    {
        // Term methods
        Task<bool> AddTermAsync(T term);
        Task<bool> UpdateTermAsync(T term);
        Task<bool> DeleteTermAsync(string id);
        Task<T> GetTermAsync(string id);
        Task<IEnumerable<T>> GetTermAsync(bool forceRefresh = false);
    }
}
