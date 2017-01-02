using EasyCredit.Infrastructure;
using EasyCredit.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyCredit.Controllers
{
    public class ClientProfileController : Controller
    {
        private EasyCreditUnitOfWork unitOfWork;
        private RequestProvider requestProvider;

        public ClientProfileController()
        {
            unitOfWork = new EasyCreditUnitOfWork();
            requestProvider = new RequestProvider(unitOfWork);
        }

        // GET: ClientProfile
        public ActionResult Index()
        {
            var user = unitOfWork.ApplicationUserRepositiry.Get(User.Identity.GetUserGuidId());

            return View(user);
        }
    }
}