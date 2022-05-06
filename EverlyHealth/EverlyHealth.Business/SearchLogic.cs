using EverlyHealth.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Business
{
    public class SearchLogic : ISearchLogic
    {
        private ISearchRepository _searchRepository;

        public SearchLogic(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public void AddKeys(List<string> keys, int owner)
        {
            _searchRepository.AddKeys(keys, owner);
        }

        public Dictionary<int, string> SearchText(string query, List<int> knownOwners)
        {
            return _searchRepository.FindText(query, knownOwners);

        }
    }
}
