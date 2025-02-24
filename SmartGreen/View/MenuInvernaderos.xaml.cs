using SmartGreen.ViewModel;

namespace SmartGreen.View;

public partial class MenuInvernaderos : ContentPage
{
	public MenuInvernaderos()
	{
		InitializeComponent();
        VMmenuInvernaderos vMmenuInvernaderos = new VMmenuInvernaderos();

        BindingContext = vMmenuInvernaderos;
    }
}