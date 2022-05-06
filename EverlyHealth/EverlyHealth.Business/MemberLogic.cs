using EverlyHealth.Models;
using EverlyHealth.Repository;
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
        public MemberLogic(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public void AddMember(Member member, string contacts)
        {
            if (!String.IsNullOrEmpty(contacts))
            {
                var contactIds = contacts.Split(',').Where(a => !String.IsNullOrEmpty(a));
                member.Contacts = _memberRepository.GetMembersByIds(contactIds.Select(x => int.Parse(x)).ToList());
            }
            _memberRepository.addMember(member);
        }

        public List<Member> GetAllMembers()
        {
            return _memberRepository.GetAllMembers();
        }
    }
}
