using SmartGreen.ViewModel;

namespace SmartGreen.View;

public partial class Register : ContentPage
{
	public Register()
    {
        InitializeComponent();
        VMRegister vmRegister = new VMRegister();

        BindingContext = vmRegister;
    }



}