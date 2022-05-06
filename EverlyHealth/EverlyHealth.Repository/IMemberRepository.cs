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
        Member addMember(Member member);
        HashSet<Member> GetMembersByIds(List<int> membersId);
        List<Member> GetAllMembers();
        Member GetMember(int id);
        Member UpdateMember(Member member);
    }
}
