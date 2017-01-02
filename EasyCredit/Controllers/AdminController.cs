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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private EasyCreditUnitOfWork unitOfWork;
        private RequestProvider requestProvider;

        public AdminController()
        {
            unitOfWork = new EasyCreditUnitOfWork();
            requestProvider = new RequestProvider(unitOfWork);
        }
    
        // GET: Admin
        public ActionResult Index()
        {
            var model = unitOfWork.ApplicationUserRepositiry.GetAll().ToList();
            return View(model);
        }

        public ActionResult DenyRequest(Guid id)
        {
            requestProvider.DenyRequest(id);
            return RedirectToAction("ShowNewRequests");
        }

        public ActionResult AcceptRequest(Guid id)
        {
            requestProvider.AcceptRequest(id);
            return RedirectToAction("ShowNewRequests");
        }

        public ActionResult ShowNewRequests()
        {
            var model = unitOfWork.RequestRepositiry.GetAll().Where(x=>x.StatusOfRequest == StatusRequestDictionary.Waiting).ToList();
            return View(model);
        }

        public ActionResult ShowDenyRequests()
        {
            var model = unitOfWork.RequestRepositiry.GetAll().Where(x => x.StatusOfRequest == StatusRequestDictionary.Denied).ToList();
            return View(model);
        }

        public ActionResult ShowAcceptedRequests()
        {
            var model = unitOfWork.RequestRepositiry.GetAll().Where(x => x.StatusOfRequest == StatusRequestDictionary.Accepted).ToList();
            return View(model);
        }
    }
}