using EverlyHealth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Repository
{
    public class  MemberRepository : IMemberRepository
    {
        private Dictionary<int, Member> _members = new Dictionary<int, Member>();

        public Member addMember(Member member)
        {
            member.Id = _members.Count;

            if (member.Contacts != null && member.Contacts.Any())
            {

                foreach (var contact in member.Contacts)
                {
                    _members[contact.Id].Contacts.Add(member);
                }
            }
            _members[member.Id] = member;       
            return member;
        }

        public HashSet<Member> GetMembersByIds(List<int> membersId)
        {
            HashSet<Member> members = new HashSet<Member>();
            foreach (var contactId in membersId)
            {

                if (_members.TryGetValue(contactId, out Member mem))
                {
                    members.Add(mem);
                }
            }
            return members;
        }

        public List<Member> GetAllMembers()
        {
            return _members.Values.ToList();
        }

        public Member GetMember(int id)
        {
            return _members.GetValueOrDefault(id);
        }
    }
}
