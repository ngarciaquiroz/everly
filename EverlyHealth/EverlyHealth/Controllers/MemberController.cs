using EverlyHealth.Business;
using EverlyHealth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EverlyHealth.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberLogic _memberLogic;
        private readonly ISearchLogic _searchLogic;

        public MemberController(IMemberLogic memberLogic, ISearchLogic searchLogic)
        {
            _memberLogic = memberLogic;
            _searchLogic = searchLogic;
        }

        // GET: MemberController
        public ActionResult Index()
        {
            return View(_memberLogic.GetAllMembers());
        }

        // GET: MemberController/View/5
        public ActionResult View(int id)
        {
            var member = _memberLogic.GetMember(id);
            return View(member);
        }

        // GET: MemberController/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Member member, string contacts)
        {
            try
            {
                _memberLogic.AddMember(member, contacts);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            var member = _memberLogic.GetMember(id);
            return View(member);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member member, string contacts)
        {
            try
            {
                _memberLogic.UpdateMember(member, contacts, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Search/5
        public ActionResult Search(int id)
        {

            return View(new Search { Id = id });
        }

        // GET: MemberController/Search/5
        [HttpPost]
        public ActionResult Search(Search search)
        {
            var currentMember = _memberLogic.GetMember(search.Id);
            var response = new Search { Id = search.Id };
            if (currentMember != null)
            {
                var knownContacts = currentMember.Contacts.Select(a => a.Id).ToList();
                knownContacts.Add(search.Id);
                var searchResults = _searchLogic.SearchText(search.Query, knownContacts);
                foreach (var contact in searchResults)
                {
                    var path = _memberLogic.GetIntroductionPath(search.Id, contact.Key);
                    response.Experts.Add(path, contact.Value);

                }
            }

            return View(response);
        }
    }
}
