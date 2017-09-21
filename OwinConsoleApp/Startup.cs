using Owin;
using Microsoft.Owin;
using System.Web.Http;
using Swashbuckle.Application;
[assembly: OwinStartup(typeof(OwinConsoleApp.Startup))]
namespace OwinConsoleApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            SwaggerConfig.Register(config);
            appBuilder.UseWebApi(config);
        }
    }
}