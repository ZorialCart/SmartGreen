using SmartGreen.ViewModel;

namespace SmartGreen.View.ViveroView;

public partial class RegistrarInvernadero : ContentPage
{
	public RegistrarInvernadero()
	{
		InitializeComponent();

		VMRegistrarInvernaderos vMRegistrarInvernaderos = new VMRegistrarInvernaderos();
        BindingContext = vMRegistrarInvernaderos;
    }
 
}