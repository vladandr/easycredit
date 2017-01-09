using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using EasyCredit.EasyCreditAssertionGroup;
using EasyCredit.Infrastructure;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using EasyCredit.Models;
using EasyCredit.Models.Identity;


namespace EasyCredit.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            userManager.ThrowIfArgumentIsNull(nameof(userManager));
            signInManager.ThrowIfArgumentIsNull(nameof(signInManager));

            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager { get; }

        public ApplicationUserManager UserManager { get; }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess
                        ? "Your password has been set."
                        : message == ManageMessageId.SetTwoFactorSuccess
                            ? "Your two-factor authentication provider has been set."
                            : message == ManageMessageId.Error
                                ? "An error has occurred."
                                : message == ManageMessageId.AddPhoneSuccess
                                    ? "Your phone number was added."
                                    : message == ManageMessageId.RemovePhoneSuccess
                                        ? "Your phone number was removed."
                                        : "";

            Guid userId = User.Identity.GetUserGuidId();
            var roles = UserManager.GetRoles(userId);
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered
                    = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId.ToString())
            };
            return View(model);
        }
        
        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IdentityResult result = await UserManager.ChangePasswordAsync(
                User.Identity.GetUserGuidId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserGuidId());

                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return RedirectToAction("Index", new {Message = ManageMessageId.ChangePasswordSuccess});
            }

            AddErrors(result);

            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword() => View();

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result =
                    await UserManager.AddPasswordAsync(User.Identity.GetUserGuidId(), model.NewPassword);
                if (result.Succeeded)
                {
                    ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserGuidId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new {Message = ManageMessageId.SetPasswordSuccess});
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        #region Helpers

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserGuidId());

            return user?.PasswordHash != null;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}