using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WGUPlanner.Models;

namespace WGUPlanner.Services
{

    public class MockDataTermStore : IDataTermStore<Term>
    {
        readonly List<Term> terms;

        public MockDataTermStore()
        {
            terms = new List<Term>()
            {
                //new Term { Id = Guid.NewGuid().ToString(), Title = "First item", Description="This is an item description.",StartDate=DateTime.Now,EndDate=DateTime.Now.AddHours(600) }
                //new Term { Id = Guid.NewGuid().ToString(), Title = "Second item", Description="This is an item description." },
                //new Term { Id = Guid.NewGuid().ToString(), Title = "Third item", Description="This is an item description." },
                //new Term { Id = Guid.NewGuid().ToString(), Title = "Fourth item", Description="This is an item description." },
                //new Term { Id = Guid.NewGuid().ToString(), Title = "Fifth item", Description="This is an item description." }
                //new Term { Id = Guid.NewGuid().ToString(), Title = "Sixth item", Description="This is an item description." }
            };
        }

        #region Term Methods **
        public async Task<bool> AddTermAsync(Term term)
        {
            terms.Add(term);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateTermAsync(Term term)
        {
            var oldTerm = terms.Where((Term arg) => arg.Id == term.Id).FirstOrDefault();
            terms.Remove(oldTerm);
            terms.Add(term);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteTermAsync(string id)
        {
            var oldTerm = terms.Where((Term arg) => arg.Id == id).FirstOrDefault();
            terms.Remove(oldTerm);

            return await Task.FromResult(true);
        }

        public async Task<Term> GetTermAsync(string id)
        {
            return await Task.FromResult(terms.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Term>> GetTermAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(terms);
        }
        #endregion
    }
}