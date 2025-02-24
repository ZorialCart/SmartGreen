using SmartGreen.ViewModel;

namespace SmartGreen.View;

public partial class AgregarInvernadero : ContentPage
{
	public AgregarInvernadero()
	{
        InitializeComponent();
        VMAgregarInvernadero vmAgregarInvernadero = new VMAgregarInvernadero();
        BindingContext = vmAgregarInvernadero;
    }
}