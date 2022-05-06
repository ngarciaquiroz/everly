using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Services
{
    public class Scrapper : IScrapper
    {

        /**
         * 
         * Adding HTMLAgilityPAck to fetch the page and use selectors  from h1-h3
         * 
         */
        public List<string> ScrapePage(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var nodes = new List<HtmlNode>();
            var headings = new List<string>();
            if (String.IsNullOrEmpty(url))
                return headings;
            
                HtmlDocument document = web.Load(url);
                for (int i = 1; i <= 3; i++)
                {
                    var tmpNodes = document.DocumentNode.SelectNodes($"//h{i}");
                    if (tmpNodes != null)
                        nodes.AddRange(tmpNodes.ToList());
                }
                headings.AddRange(nodes.Select(node => node.InnerHtml).ToList());
                return headings;
        }
    }
}
