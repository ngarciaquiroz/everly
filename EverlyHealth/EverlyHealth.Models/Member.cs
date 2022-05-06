using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Models
{
    public class Member : IEquatable<Member>
    {
        public Member()
        {

            Contacts = new HashSet<Member>();
            Headings = new List<String>();
        }

        public Member(string name, string website, string shortenUrl)
        {
            Name = name;
            Website = website;
            ShortenUrl = shortenUrl;
            Contacts = new HashSet<Member>();
            Headings = new List<String>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public HashSet<Member> Contacts { get; set; }
        public List<String> Headings { get; set; }
        public string ShortenUrl { get; set; }


        public bool Equals(Member other)
        {
            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.Name.GetHashCode();
        }
    }
}
