using EverlyHealth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Repository
{
    public class MemberRepository : IMemberRepository
    {
        /**
        * This Dictionary is acting as in-memory storage
        * Ideally this could be moved some place else for permanent storage
        * that's why I expose it using an interface to be able to swap it
        * 
        */

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

        public Member UpdateMember(Member member)
        {
            if (member.Contacts != null && member.Contacts.Any() && _members.ContainsKey(member.Id))
            {
                var currentContacts = _members[member.Id].Contacts;
                var newContacts = member.Contacts.Except(currentContacts);
                var deleteContacts = currentContacts.Except(member.Contacts);
                foreach (var contact in newContacts)
                {
                    _members[contact.Id].Contacts.Add(member);
                }
                foreach (var contact in deleteContacts)
                {
                    _members[contact.Id].Contacts.Remove(member);
                }
            }
            _members[member.Id] = member;
            return _members[member.Id];
        }
    }
}
