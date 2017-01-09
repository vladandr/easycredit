using EasyCredit.Infrastructure;
using EasyCredit.Models;
using EasyCredit.UnitOfWork;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EasyCredit.Controllers
{
    public class CreditPlanController : Controller
    {
        private EasyCreditUnitOfWork unitOfWork;
        private CreditPlanProvider creditPlanProvider;
        private RequestProvider requestProvider;

        public CreditPlanController()
        {
            unitOfWork = new EasyCreditUnitOfWork();
            creditPlanProvider = new CreditPlanProvider(unitOfWork);
            requestProvider = new RequestProvider(unitOfWork);
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public ActionResult EditCreditPlan(Guid id)
        {
            var plan = unitOfWork.CreditPlansRepository.Get(id);
            return View(plan);
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public ActionResult EditCreditPlan(CreditPlan plan)
        {
            creditPlanProvider.InsertOrUpdateCreditPlan(plan);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public ActionResult CreatePlan()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public ActionResult CreatePlan(CreditPlan plan)        
        {
            creditPlanProvider.InsertOrUpdateCreditPlan(plan);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ShowPlan(Guid id)
        {
            var plan = unitOfWork.CreditPlansRepository.Get(id);
            return View(plan);
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DeleteCreditPlan(Guid id)
        {
            creditPlanProvider.DeleteCreditPlan(id);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult SendToArchiveCreditPlan(Guid id)
        {
            creditPlanProvider.SendToArchiveCreditPlan(id);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult SendRequest(Guid id)
        {
            var userId = User.Identity.GetUserGuidId();
            requestProvider.SendRequest(userId, id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ShowHistoryCreditPlans()
        {
            var plansToShow = unitOfWork.CreditPlansRepository.
                GetAll().ToList().
                Where(x=>x.Status == Constants.CreditPlanStatusDictionary.InHistory).
                Take(5);
            return PartialView(plansToShow);
        }
    }
}