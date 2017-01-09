using EasyCredit.Infrastructure;
using EasyCredit.Models.Identity;
using EasyCredit.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyCredit.Controllers
{
    [Authorize]
    public class ClientProfileController : Controller
    {
        private EasyCreditUnitOfWork unitOfWork;
        private RequestProvider requestProvider;
        private UserProvider userProvider;

        public ClientProfileController()
        {
            unitOfWork = new EasyCreditUnitOfWork();
            requestProvider = new RequestProvider(unitOfWork);
            userProvider = new UserProvider(unitOfWork);
        }

        // GET: ClientProfile
        public ActionResult Index(Guid id)
        {
            if (User.IsInRole("Admin")||User.IsInRole("Moderator") || id == User.Identity.GetUserGuidId())
            {
                var user = unitOfWork.ApplicationUserRepositiry.Get(id);
                return View(user);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EditUser(Guid id)
        {
            if (User.IsInRole("Admin") || id == User.Identity.GetUserGuidId())
            {
                var user = unitOfWork.ApplicationUserRepositiry.Get(id);
                return View(user);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditUser(ApplicationUser user)
        {
            userProvider.EditUser(user);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowRequests(Guid userId)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator") || userId == User.Identity.GetUserGuidId())
            {
                var requests = userProvider.ShowUserRequests(userId);
                return View(requests);
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}