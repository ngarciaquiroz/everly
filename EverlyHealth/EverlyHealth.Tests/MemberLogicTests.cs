using EverlyHealth.Business;
using EverlyHealth.Models;
using EverlyHealth.Repository;
using EverlyHealth.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EverlyHealth.Tests
{
    public class MemberLoficTests
    {
        private Mock<IMemberRepository> _memberRepository;
        private Mock<IScrapper> _scrapper;
        private Mock<ITinyUrl> _tinyUrl;
        private Mock<ISearchLogic> _searchLogic;

        private List<Member> _mockMembers;
        private List<string> heading;

        [SetUp]
        public void Setup()
        {
            _memberRepository = new Mock<IMemberRepository>();
            _scrapper = new Mock<IScrapper>();
            _tinyUrl = new Mock<ITinyUrl>();
            _searchLogic = new Mock<ISearchLogic>();
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

            _mockMembers = new List<Member> { member0, member1, member2, member3, member4 };
        }

        [Test]
        public void TestGetIntroductionPath()
        {


            var memberLogic = new MemberLogic(_memberRepository.Object, _scrapper.Object, _tinyUrl.Object, _searchLogic.Object);
            _memberRepository.Setup(m => m.GetMember(0)).Returns(_mockMembers[0]);
            _memberRepository.Setup(m => m.GetMember(3)).Returns(_mockMembers[3]);

            var path = memberLogic.GetIntroductionPath(0, 3);

            _memberRepository.Verify(m => m.GetMember(0));
            _memberRepository.Verify(m => m.GetMember(3));
            Assert.That(path, Is.EqualTo("Nicolas-> Alejandra -> Carlos -> Emmanuel"));
            Assert.Pass();
        }

        [Test]
        public void TestGetIntroductionPathNoPathFound()
        {

            var memberLogic = new MemberLogic(_memberRepository.Object, _scrapper.Object, _tinyUrl.Object, _searchLogic.Object);
            _memberRepository.Setup(m => m.GetMember(0)).Returns(_mockMembers[0]);
            _memberRepository.Setup(m => m.GetMember(4)).Returns(_mockMembers[4]);

            var path = memberLogic.GetIntroductionPath(0, 4);

            _memberRepository.Verify(m => m.GetMember(0));
            _memberRepository.Verify(m => m.GetMember(4));
            Assert.That(path, Is.EqualTo("No path Found"));
            Assert.Pass();
        }

        [Test]
        public void TestAddMember()
        {
            var memberLogic = new MemberLogic(_memberRepository.Object, _scrapper.Object, _tinyUrl.Object, _searchLogic.Object);
            _tinyUrl.Setup(m => m.ShortenUrl("https://www.everlywell.com/")).Returns(Task.FromResult("https://faketiny.com/1234"));
            _scrapper.Setup(m => m.ScrapePage("https://www.everlywell.com/")).Returns(heading);
            _searchLogic.Setup(m => m.AddKeys(heading, It.IsAny<int>()));         
            var member = new Member { Name = "Pablo", Website = "https://www.everlywell.com/", Id=5 };
            _memberRepository.Setup(m => m.addMember(member)).Returns(member);
            
            memberLogic.AddMember(member, "0,1");


            _tinyUrl.Verify(m => m.ShortenUrl("https://www.everlywell.com/"));
            _scrapper.Verify(m => m.ScrapePage("https://www.everlywell.com/"));
            _searchLogic.Verify(m => m.AddKeys(heading, It.IsAny<int>()));
            _memberRepository.Setup(m => m.addMember(member));
            Assert.Pass();
        }
    }
}