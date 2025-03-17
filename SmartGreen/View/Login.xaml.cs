using SmartGreen.ViewModel;
using SmartGreen.View;
using Microsoft.Maui.Controls;
using SmartGreen.View.ViveroView;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.Xaml;
using SmartGreen.Clases;
using Microsoft.Maui.Controls;


namespace SmartGreen.View;
public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();

        VMLogin vMLogin = new VMLogin();
        BindingContext = vMLogin;
    }




    protected override bool OnBackButtonPressed()
    {
        VMLogin vm = new VMLogin();
        BindingContext = vm;

        if (vm.IsLoggedIn)
        {
            return true;
        }
        return base.OnBackButtonPressed();
    }
}
 
