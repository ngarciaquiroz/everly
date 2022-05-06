using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Repository
{
    public class SearchRepository : ISearchRepository
    {

        /**
        * This Dictionary is acting as in-memory search engine
        * in othe conditions we could use something like and index to search for the text 
        * 
        */

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
            //filter the known contacts for the member

            var unknowPeople = _topics.Where(p => !knownOwners.Contains(p.Key));
            var response = new Dictionary<int, string>();

            //iterate the rest to get which members know about it
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
