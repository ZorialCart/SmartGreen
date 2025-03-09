using SmartGreen.ViewModel;
using SmartGreen.View;
using Microsoft.Maui.Controls;
using SmartGreen.View.ViveroView;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.Xaml;

namespace SmartGreen.View;
public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();

        VMLogin vMLogin = new VMLogin();
        BindingContext = vMLogin;

    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        var vm = (VMLogin)BindingContext;

        // Si el usuario ha iniciado sesi�n, navegar a la p�gina principal y evitar el retroceso
        if (vm.IsLoggedIn)
        {
            // Reemplazamos la p�gina de Login con la de MenuView
            Shell.Current.GoToAsync("//MenuView", true); // true reemplaza la pila de navegaci�n
        }
    }



}
