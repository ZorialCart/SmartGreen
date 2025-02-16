using SmartGreen.ViewModel;

namespace SmartGreen.View;

public partial class MenuInvernaderos : ContentPage
{
	public MenuInvernaderos()
	{
		InitializeComponent();
        BindingContext = new VMinvernaderos(Navigation);
    }
}