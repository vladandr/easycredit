using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

using EasyCredit.Models.Identity;

using Microsoft.AspNet.Identity.EntityFramework;
using EasyCredit.Models;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;

namespace EasyCredit.Contexts
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
            ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext() : base("name=EasyCreditEntities")
        {
            Database.SetInitializer(new MyContextInitializer());
        }

        #region DbSets

        //    public DbSet<Children> Children { get; set; }

        public DbSet<Request> Request { get; set; }

        //    public DbSet<Credit> Credits { get; set; }

        //    public DbSet<BankAccount> BankAccounts { get; set; }

        //    public DbSet<BankCard> BankCards { get; set; }

        //    public DbSet<ClientRating> ClientRating { get; set; }

        public DbSet<CreditPlan> CreditPlan { get; set; }

        //    public DbSet<Repayment> Repayment { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            IEnumerable<Type> typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (dynamic configurationInstance in typesToRegister.Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }

        class MyContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Roles.AddOrUpdate(role => role.Name,
                    new ApplicationRole { Name = "Admin" },
                    new ApplicationRole { Name = "Moderator" },
                    new ApplicationRole { Name = "Client" });
                var userManager = new ApplicationUserManager(new ApplicationUserStore(context));

                var admin = new ApplicationUser
                {
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    LockoutEnabled = false,
                };
                string adminPassword = "1qaz@WSXa";
                var result = userManager.Create(admin, adminPassword);


                var manager = new ApplicationUser
                {
                    Email = "manager@manager.com",
                    UserName = "manager@manager.com",
                    LockoutEnabled = true,
                };

                string managerPassword = "1qaz@WSXm";
                var result1 = userManager.Create(manager, managerPassword);


                var user = new ApplicationUser
                {
                    Email = "user@user.com",
                    UserName = "user@user.com",
                    LockoutEnabled = true,
                };

                string userPassword = "1qaz@WSXu";
                var result2 = userManager.Create(user, userPassword);


                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, "Admin");
                }

                if (result1.Succeeded)
                {
                    userManager.AddToRole(manager.Id, "Moderator");
                }
                context.SaveChanges();
            }
        }

    }
}
