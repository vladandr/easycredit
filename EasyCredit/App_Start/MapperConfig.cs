using AutoMapper;
using AutoMapper.Configuration;

using EasyCredit.Models;
using EasyCredit.Models.Identity;


namespace EasyCredit
{
    public class MapperConfig
    {
        public static void Config()
        {
            var expression = new MapperConfigurationExpression();

            expression.CreateMap<ApplicationRole, RoleModel>();

            Mapper.Initialize(expression);
        }
    }
}
