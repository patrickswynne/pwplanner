
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WGUPlanner.Models;

namespace WGUPlanner.Services
{
    public class MockDataAssessmentStore : IDataAssessmentStore<Assessment>
    {
        readonly List<Assessment> assessments;

        public MockDataAssessmentStore()
        {
            assessments = new List<Assessment>();
        }

        #region Assessment Methods **
        public async Task<bool> AddAssessmentAsync(Assessment assessment)
        {
            assessments.Add(assessment);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAssessmentAsync(Assessment assessment)
        {
            var oldAssessment = assessments.Where((Assessment arg) => arg.Id == assessment.Id).FirstOrDefault();
            assessments.Remove(oldAssessment);
            assessments.Add(assessment);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAssessmentAsync(string id)
        {
            var oldAssessment = assessments.Where((Assessment arg) => arg.Id == id).FirstOrDefault();
            assessments.Remove(oldAssessment);

            return await Task.FromResult(true);
        }

        public async Task<Assessment> GetAssessmentAsync(string id)
        {
            return await Task.FromResult(assessments.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Assessment>> GetAssessmentAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(assessments);
        }
        #endregion
    }
}
