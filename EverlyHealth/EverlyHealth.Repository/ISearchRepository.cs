using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Repository
{
    public interface ISearchRepository
    {
        void AddKeys(List<string> keys, int owner);
        Dictionary<int, string> FindText(string query, List<int> knownOwners);
    }
}
