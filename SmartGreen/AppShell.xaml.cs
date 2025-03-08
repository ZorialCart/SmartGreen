using SmartGreen.View;
using SmartGreen.View.RecoveryPass;
using SmartGreen.View.ViveroView;
using SmartGreen.ViewModel;
namespace SmartGreen
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            VMLogin vMLogin = new VMLogin();
            BindingContext = vMLogin;

    
            Routing.RegisterRoute("Login", typeof(Login));
            Routing.RegisterRoute("MenuView", typeof(MenuView));
            Routing.RegisterRoute("Register", typeof(Register));
            Routing.RegisterRoute("Recovery1", typeof (Recovery1));
            Routing.RegisterRoute("Recovery2", typeof(Recovery2));

        }
    }
}
