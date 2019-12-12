using System.Collections.Generic;
using System.Threading.Tasks;

namespace WGUPlanner.Services
{
    public interface IDataCourseStore<T>
    {
        // Course methods
        Task<bool> AddCourseAsync(T course);
        Task<bool> UpdateCourseAsync(T course);
        Task<bool> DeleteCourseAsync(string id);
        Task<T> GetCourseAsync(string id);
        Task<IEnumerable<T>> GetCourseAsync(bool forceRefresh = false);

    }
}
