using System.Linq;
using AutoMapper;
using EasyCredit.EasyCreditAssertionGroup;
using EasyCredit.Models;
using EasyCredit.Models.Identity;
using EasyCredit.Repositories;


namespace EasyCredit.Services
{
    public class RoleService
    {
        public const string AdminRoleName = "admin";
        public const string ManagerRoleName = "manager";
        public const string ClientRoleName = "client";

        private static ApplicationRole _admin;
        private static ApplicationRole _manager;
        private static ApplicationRole _client;

        public RoleService(IRepository<ApplicationRole> roleRepository)
        {
            roleRepository.ThrowIfArgumentIsNull(nameof(roleRepository));

            RoleRepository = roleRepository;
        }

        protected IRepository<ApplicationRole> RoleRepository { get; }

        public RoleModel Admin => Mapper.Map<RoleModel>(AdminRole);

        public RoleModel Manager => Mapper.Map<RoleModel>(ManagerRole);

        public RoleModel Client => Mapper.Map<RoleModel>(ClientRole);

        internal ApplicationRole AdminRole => _admin ?? (_admin = FindRole(AdminRoleName));

        internal ApplicationRole ManagerRole => _manager ?? (_manager = FindRole(ManagerRoleName));

        internal ApplicationRole ClientRole => _client ?? (_client = FindRole(ClientRoleName));

        private ApplicationRole FindRole(string roleName)
        {
            return RoleRepository.GetAll().First(role => role.Name == roleName);
        }
    }
}
