using EverlyHealth.Models;
using EverlyHealth.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverlyHealth.Tests
{
    public class SearchRepositoryTest
    {

        private List<Member> _mockMembers;
        private List<string> heading, heading2, heading3;


        [SetUp]
        public void Setup()
        {

            var member0 = new Member { Name = "Nicolas", Id = 0 };
            var member1 = new Member { Name = "Alejandra", Id = 1 };
            var member2 = new Member { Name = "Carlos", Id = 2 };
            var member3 = new Member { Name = "Emmanuel", Id = 3 };
            var member4 = new Member { Name = "Pablo", Id = 4 };

            member0.Contacts.Add(member1);

            member1.Contacts.Add(member0);
            member1.Contacts.Add(member2);

            member2.Contacts.Add(member1);
            member2.Contacts.Add(member3);

            member3.Contacts.Add(member2);


            heading = new List<string> { "Fake Heading1", "Fake Heading2", "Fake Heading3", "Fake Heading4" };
            heading2 = new List<string> { "Ut viverra maximus cursus. M", "quam ac egestas sodales, nequ", "Sed eu sem id odio condimentu", "eger venenatis cursus " };
            heading3 = new List<string> { "consectetur adipiscing elit. Mauris sed turpis dui. Cras",
                "consectetur adipiscing elit. Mauris sed turpis dui. Cras"
                , "non fringilla augue pharetra" };

            _mockMembers = new List<Member> { member0, member1, member2, member3, member4 };
        }

        [Test]
        public void TestFindText()
        {


            var searchRepository = new SearchRepository();
            searchRepository.AddKeys(heading, 0);
            searchRepository.AddKeys(heading2, 1);
            searchRepository.AddKeys(heading3, 2);
            var result = searchRepository.FindText("egestas sodales", new List<int> { 0 });
            Assert.That(result.Values.First(), Is.EqualTo("quam ac egestas sodales, nequ"));
            Assert.That(result.Keys.First(), Is.EqualTo(1));
            Assert.Pass();
        }
    }
}
