using EverlyHealth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Business
{
    public interface IMemberLogic
    {
        void AddMember(Member member, string contacts);
        List<Member> GetAllMembers();
    }
}
