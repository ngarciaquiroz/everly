using EverlyHealth.Business;
using EverlyHealth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EverlyHealth.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberLogic _memberLogic;

        public MemberController(IMemberLogic memberLogic)
        {
            _memberLogic = memberLogic;
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
                _memberLogic.AddMember(member,contacts);
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
            return View();
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
