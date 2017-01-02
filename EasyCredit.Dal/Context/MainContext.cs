using EasyCredit.DAL.Models;
using System.Data.Entity;

namespace EasyCredit.DAL.Context
{
    public class MainContext : DbContext
    {
        public MainContext() : base("DefaultConnection") { }
        public DbSet<Children> Childrens { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Credit> Credits { get; set; }
        

    }
}
