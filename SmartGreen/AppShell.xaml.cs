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
            Routing.RegisterRoute("RegistrarInvernadero", typeof(RegistrarInvernadero));
            Routing.RegisterRoute("GreenHouse",typeof(GreenHouseView));

            if(!UserIsLoggedIn())
            {
                GoToAsync("Login", true);

            }
            
        }

       protected bool UserIsLoggedIn()
        {
            var vm = (VMLogin)BindingContext;
            return vm.IsLoggedIn;
        }
    }
}

