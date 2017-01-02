using EasyCredit.Infrastructure;
using EasyCredit.Models;
using EasyCredit.UnitOfWork;
using System;
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

        [HttpGet]
        public ActionResult EditCreditPlan(Guid id)
        {
            var plan = unitOfWork.CreditPlansRepository.Get(id);
            return View(plan);
        }

        [HttpPost]
        public ActionResult EditCreditPlan(CreditPlan plan)
        {
            creditPlanProvider.InsertOrUpdateCreditPlan(plan);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult CreatePlan()
        {
            return View();
        }        

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

        public ActionResult DeleteCreditPlan(Guid id)
        {
            creditPlanProvider.DeleteCreditPlan(id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SendToArchiveCreditPlan(Guid id)
        {
            creditPlanProvider.SendToArchiveCreditPlan(id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SendRequest(Guid id)
        {
            var userId = User.Identity.GetUserGuidId();
            requestProvider.SendRequest(userId, id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ShowRecentlyCreatedCreditPlans()
        {
            var plansToShow = creditPlanProvider.ShowRecentlyCreatedCreditPlans();
            return Json(plansToShow);
        }
    }
}