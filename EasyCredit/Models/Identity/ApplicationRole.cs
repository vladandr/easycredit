using System;

using Microsoft.AspNet.Identity.EntityFramework;
using EasyCredit.Contexts;

namespace EasyCredit.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>, IEasyCreditEntity
    {
    }

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, Guid,
    ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole, Guid, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
