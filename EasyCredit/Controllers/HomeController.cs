using EasyCredit.Constants;
using EasyCredit.DAL.Attributes;
using EasyCredit.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyCredit.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private EasyCreditUnitOfWork unitOfWork;

        public HomeController()
        {
            unitOfWork = new EasyCreditUnitOfWork();
        }
        public ActionResult Index()
        {
            var model = unitOfWork.CreditPlansRepository.GetAll().ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}