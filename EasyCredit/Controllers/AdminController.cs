using EasyCredit.Constants;
using EasyCredit.Infrastructure;
using EasyCredit.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyCredit.Controllers
{
    public class AdminController : Controller
    {
        private EasyCreditUnitOfWork unitOfWork;
        private RequestProvider requestProvider;
        private AdminProvider adminProvider;


        public AdminController()
        {
            unitOfWork = new EasyCreditUnitOfWork();
            requestProvider = new RequestProvider(unitOfWork);
            adminProvider = new AdminProvider(unitOfWork);
        }

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = unitOfWork.ApplicationUserRepositiry.GetAll().ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DenyRequest(Guid id)
        {
            requestProvider.DenyRequest(id);
            return RedirectToAction("ShowNewRequests");
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult AcceptRequest(Guid id)
        {
            requestProvider.AcceptRequest(id);
            return RedirectToAction("ShowNewRequests");
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult ShowNewRequests()
        {
            var model = unitOfWork.RequestRepositiry.GetAll().Where(x=>x.StatusOfRequest == StatusRequestDictionary.Waiting).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult ShowDenyRequests()
        {
            var model = unitOfWork.RequestRepositiry.GetAll().Where(x => x.StatusOfRequest == StatusRequestDictionary.Denied).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult ShowAcceptedRequests()
        {
            var model = unitOfWork.RequestRepositiry.GetAll().Where(x => x.StatusOfRequest == StatusRequestDictionary.Accepted).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Ban(Guid id)
        {
            adminProvider.Ban(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Unban(Guid id)
        {
            adminProvider.Unban(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ResetPassword(Guid id)
        {
            adminProvider.ResetToDefaultPassword(id);
            return RedirectToAction("Index");
        }

    }
}