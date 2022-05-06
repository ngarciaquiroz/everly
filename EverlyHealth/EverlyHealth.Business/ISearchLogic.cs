using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Business
{
    public interface ISearchLogic
    {
        Dictionary<int, string> SearchText(string query, List<int> knownOwners);
        void AddKeys(List<string> keys, int owner);
    }
}
