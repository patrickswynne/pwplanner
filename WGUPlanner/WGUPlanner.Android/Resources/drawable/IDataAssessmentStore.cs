using System.Collections.Generic;
using System.Threading.Tasks;

namespace WGUPlanner.Services
{
    public interface IDataAssessmentStore<T>
    {
        // Assessment methods
        Task<bool> AddAssessmentAsync(T assessment);
        Task<bool> UpdateAssessmentAsync(T assessment);
        Task<bool> DeleteAssessmentAsync(string id);
        Task<T> GetAssessmentAsync(string id);
        Task<IEnumerable<T>> GetAssessmentAsync(bool forceRefresh = false);

    }
}
