using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using EasyCredit.EasyCreditAssertionGroup;
using EasyCredit.Infrastructure;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

using EasyCredit.Models.Identity;

using Microsoft.Owin.Security.DataProtection;


namespace EasyCredit
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. 
    // UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store)
           : base(store)
        {
        }

        public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store, 
            IdentityFactoryOptions<ApplicationUserManager> options)
            : base(store)
        {
            // Configure validation logic for usernames
            UserValidator = new UserValidator<ApplicationUser, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(15);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Emails as a 
            // step of receiving a code for verifying the user 
            // You can write your own provider and plug it in here.
            //RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, Guid>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            //EmailService = new EmailService();

            RegisterTwoFactorProvider(
                Constants.EasyCreditConstants.CardCodeProvider, new CodeCardTokenProvider());


            IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");
                
                UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, Guid>(dataProtector);
            }
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            user.ThrowIfArgumentIsNull(nameof(user));

            user.CardCodes = string.Join(";", Enumerable.Range(1, 9).Select(i => i * 10 + i));

            return await base.CreateAsync(user);
        }

        public async Task<IReadOnlyList<int>> GetAvailableCardCodesAsync(Guid userId)
        {
            ApplicationUser user = await FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            return GetAvailableCardCodes(user);
        }

        public IReadOnlyList<int> GetAvailableCardCodes(ApplicationUser user)
        {
            user.ThrowIfArgumentIsNull(nameof(user));

            return user.CardCodes.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, Guid>
    {
        public ApplicationSignInManager(
            ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public async Task<string> GetCardCodeIndexAsync(string provider)
        {
            Guid userId = await GetVerifiedUserIdAsync();

            return await UserManager.GenerateTwoFactorTokenAsync(userId, provider);
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public override string ConvertIdToString(Guid id)
        {
            return id.ToString();
        }

        public override Guid ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(Guid);
            }
            
            return Guid.Parse(id);
        }

        public static ApplicationSignInManager Create(
            IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            ApplicationUserManager userManager = context.GetUserManager<ApplicationUserManager>();

            return new ApplicationSignInManager(userManager, context.Authentication);
        }
    }
}
