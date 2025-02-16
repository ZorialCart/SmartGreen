using SmartGreen.View;
using SmartGreen.View.ViveroView;
namespace SmartGreen
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(Login));
            Routing.RegisterRoute("MenuView", typeof(MenuView));

        }
    }
}
