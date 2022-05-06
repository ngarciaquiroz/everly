using EverlyHealth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Repository
{
    public interface IMemberRepository
    {
        void addMember(Member member);
        HashSet<Member> GetMembersByIds(List<int> membersId);
        List<Member> GetAllMembers();
    }
}
