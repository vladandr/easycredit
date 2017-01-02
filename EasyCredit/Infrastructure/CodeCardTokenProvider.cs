using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EasyCredit.Constants;
using EasyCredit.EasyCreditAssertionGroup;
using EasyCredit.Models.Identity;

using Microsoft.AspNet.Identity;


namespace EasyCredit.Infrastructure
{
    public class CodeCardTokenProvider : IUserTokenProvider<ApplicationUser, Guid>
    {
        private static readonly Random Random = new Random();

        public virtual Task<bool> IsValidProviderForUserAsync(
            UserManager<ApplicationUser, Guid> manager, ApplicationUser user)
        {
            user.ThrowIfArgumentIsNull(nameof(user));
            ApplicationUserManager userManager = GetUserManager(manager);

            return Task.FromResult(userManager.GetAvailableCardCodes(user) != null);
        }

        public async Task<string> GenerateAsync(
            string purpose, UserManager<ApplicationUser, Guid> manager, ApplicationUser user)
        {
            user.ThrowIfArgumentIsNull(nameof(user));
            ApplicationUserManager userManager = GetUserManager(manager);

            IReadOnlyList<int> cardCodes = userManager.GetAvailableCardCodes(user);
            int index = Random.Next(cardCodes.Count);

            user.SelectedCardCode = cardCodes[index];
            user.CardCodeIssued = DateTime.Now;

            await userManager.UpdateAsync(user);

            return index.ToString();
        }

        public Task<bool> ValidateAsync(
            string purpose, string token, UserManager<ApplicationUser, Guid> manager, ApplicationUser user)
        {
            user.ThrowIfArgumentIsNull(nameof(user));

            if (!user.CardCodeIssued.HasValue)
            {
                return Task.FromResult(false);
            }

            int code;
            if (!int.TryParse(token, out code))
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(user.SelectedCardCode == code 
                /*&& user.CardCodeIssued.Value < DateTime.Now + EasyCreditConstants.CodeCardInterval*/);
        }

        public virtual Task NotifyAsync(
            string token, UserManager<ApplicationUser, Guid> manager, ApplicationUser user)
        {
            throw new InvalidOperationException(nameof(NotifyAsync));
        }

        private static ApplicationUserManager GetUserManager(UserManager<ApplicationUser, Guid> manager)
        {
            manager.ThrowIfArgumentIsNull(nameof(manager));
            return (ApplicationUserManager)manager;
        }
    }
}
