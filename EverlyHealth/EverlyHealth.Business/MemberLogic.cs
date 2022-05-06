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
                    _searchLogic.AddKeys(member.Headings, member.Id);
                }
                if (!String.IsNullOrEmpty(contacts))
                {
                    var contactIds = contacts.Split(',').Where(a => !String.IsNullOrEmpty(a));
                    member.Contacts = _memberRepository.GetMembersByIds(contactIds.Select(x => int.Parse(x)).ToList());
                }
                _memberRepository.addMember(member);
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
            return "Path";
        }

        public Member GetMember(int id)
        {
            return _memberRepository.GetMember(id);
        }


    }
}
