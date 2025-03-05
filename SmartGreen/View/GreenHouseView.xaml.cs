using SmartGreen.Model;
using SmartGreen.ViewModel;

namespace SmartGreen.View;

public partial class GreenHouseView : ContentPage
{
	public GreenHouseView()
	{
		InitializeComponent();
        BindingContext = new VMGreenHouseView(new InverStatusModel());
    }
    private async void OnOpenPopupClicked(object sender, EventArgs e)
    {
        await Humedad.Show();// Muestra el popup
    }
    private void OnSwitchToggled(object sender, ToggledEventArgs e)
    {
        var viewModel = BindingContext as VMGreenHouseView;
        viewModel?.ToggledCommand.Execute(e.Value);
    }
}