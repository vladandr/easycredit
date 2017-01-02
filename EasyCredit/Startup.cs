using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EasyCredit.Startup))]
namespace EasyCredit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            DependenciesConfig.RegisterDependencies(builder);

            ConfigureAuth(app, builder);

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
