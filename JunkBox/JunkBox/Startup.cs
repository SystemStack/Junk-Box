using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JunkBox.Startup))]
namespace JunkBox {
    public partial class Startup {
        public void Configuration (IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
