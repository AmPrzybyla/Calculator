using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalculatorFP.Startup))]
namespace CalculatorFP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
