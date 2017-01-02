using System;

using Microsoft.AspNet.Identity.EntityFramework;


namespace EasyCredit.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>, IEasyCreditEntity
    {
    }
}
