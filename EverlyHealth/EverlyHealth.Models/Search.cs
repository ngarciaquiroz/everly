using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Models
{
    public class Search
    {
        public Search()
        {
            Experts = new Dictionary<string, string>();
        }
        public string Query { get; set; }
        public int Id { get; set; }
        public Dictionary<string,string> Experts { get; set; }
    }
}
