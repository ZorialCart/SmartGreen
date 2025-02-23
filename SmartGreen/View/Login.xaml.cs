using SmartGreen.ViewModel;
using SmartGreen.View;
namespace SmartGreen.View;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();

        VMLogin vMLogin = new VMLogin();
        BindingContext = vMLogin;

        }
}
