using EverlyHealth.Models;
using EverlyHealth.Repository;
using EverlyHealth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Business
{
    public class MemberLogic : IMemberLogic
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IScrapper _scrapper;
        private readonly ITinyUrl _tinyUrl;
        private readonly ISearchLogic _searchLogic;
        public MemberLogic(IMemberRepository memberRepository, IScrapper scrapper, ITinyUrl tinyUrl, ISearchLogic searchLogic)
        {
            _memberRepository = memberRepository;
            _scrapper = scrapper;
            _tinyUrl = tinyUrl;
            _searchLogic = searchLogic;
        }

        public void AddMember(Member member, string contacts)
        {
            try
            {
                if (member != null && !String.IsNullOrEmpty(member.Website))
                {
                    member.Headings = _scrapper.ScrapePage(member.Website);
                    member.ShortenUrl = _tinyUrl.ShortenUrl(member.Website).Result;

                }
                if (!String.IsNullOrEmpty(contacts))
                {
                    var contactIds = contacts.Split(',').Where(a => !String.IsNullOrEmpty(a));
                    member.Contacts = _memberRepository.GetMembersByIds(contactIds.Select(x => int.Parse(x)).ToList());
                }
                var newMember = _memberRepository.addMember(member);
                _searchLogic.AddKeys(member.Headings, newMember.Id);
            }
            catch (UriFormatException e)
            {
                throw new Exception("Invalid Uri");
                System.Console.WriteLine(e.Message);
            }

        }

        public List<Member> GetAllMembers()
        {
            return _memberRepository.GetAllMembers();
        }

        public string GetIntroductionPath(int from, int to)
        {
            var startingMember = GetMember(from);
            var targetMember = GetMember(to);
            var path = GetPath(startingMember, targetMember, new List<int>());
            if (String.IsNullOrEmpty(path))
            {
                return "No path Found";
            }
            else
            {
                return startingMember.Name + "-> " + path;
            }
        }

        private string GetPath(Member from, Member to, List<int> visited)
        {
            if (visited.Contains(from.Id))
            {
                return "";
            }

            visited.Add(from.Id);
            foreach (var contact in from.Contacts)
            {
                if (!visited.Contains(contact.Id))
                {
                    if (contact.Id == to.Id)
                    {
                        visited.Add(contact.Id);
                        return contact.Name;
                    }
                    else
                    {
                        var path = GetPath(contact, to, visited);
                        visited.Add(contact.Id);
                        if (!String.IsNullOrEmpty(path))
                        {
                            return $"{contact.Name} -> {path}";
                        }
                    }

                }

            }
            return "";
        }

        public Member GetMember(int id)
        {
            return _memberRepository.GetMember(id);
        }


    }
}
