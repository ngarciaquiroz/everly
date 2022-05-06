using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private Dictionary<int, List<string>> _topics;

        public SearchRepository()
        {
            _topics = new Dictionary<int, List<string>>();
        }
        public void AddKeys(List<string> keys, int owner)
        {
            _topics[owner] = keys;
        }

        public Dictionary<int, string> FindText(string query, List<int> knownOwners)
        {
            var unknowPeople = _topics.Where(p => !knownOwners.Contains(p.Key));
            var response = new Dictionary<int, string>();
            foreach (var people in unknowPeople)
            {
                var foundMatch = people.Value.Find(a => a.Contains(query, StringComparison.InvariantCultureIgnoreCase));
                if (foundMatch != null)
                {
                    response.Add(people.Key, foundMatch);
                    continue;
                }
            }
            return response;

        }
    }
}
