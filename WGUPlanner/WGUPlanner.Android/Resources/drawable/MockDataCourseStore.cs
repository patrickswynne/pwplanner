using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WGUPlanner.Models;

namespace WGUPlanner.Services
{
    public class MockDataCourseStore : IDataCourseStore<Course>
    {
        readonly List<Course> courses;

        public MockDataCourseStore()
        {
            courses = new List<Course>();
        }
        #region Course Methods **
        public async Task<bool> AddCourseAsync(Course course)
        {
            courses.Add(course);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            var oldCourse = courses.Where((Course arg) => arg.Id == course.Id).FirstOrDefault();
            courses.Remove(oldCourse);
            courses.Add(course);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteCourseAsync(string id)
        {
            var oldCourse = courses.Where((Course arg) => arg.Id == id).FirstOrDefault();
            courses.Remove(oldCourse);

            return await Task.FromResult(true);
        }

        public async Task<Course> GetCourseAsync(string id)
        {
            return await Task.FromResult(courses.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Course>> GetCourseAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(courses);
        }
        #endregion
    }
}
