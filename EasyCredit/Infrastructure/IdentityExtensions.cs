using System;
using System.Security.Principal;

using EasyCredit.EasyCreditAssertionGroup;

using Microsoft.AspNet.Identity;


namespace EasyCredit.Infrastructure
{
    public static class IdentityExtensions
    {
        public static Guid GetUserGuidId(this IIdentity identity)
        {
            identity.ThrowIfArgumentIsNull(nameof(identity));

            string userId = identity.GetUserId();

            return Guid.Parse(userId);
        }
    }
}
