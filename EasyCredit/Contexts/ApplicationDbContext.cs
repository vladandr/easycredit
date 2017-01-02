using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

using EasyCredit.Migrations;
using EasyCredit.Models.Identity;

using Microsoft.AspNet.Identity.EntityFramework;
using EasyCredit.Models;

namespace EasyCredit.Contexts
{
    public class ApplicationDbContext 
        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, 
            ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext() : base("name=EasyCreditEntities")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
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
    }
}
