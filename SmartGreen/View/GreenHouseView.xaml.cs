using SmartGreen.Model;
using SmartGreen.ViewModel;
using System.Text.Json;

namespace SmartGreen.View;
[QueryProperty(nameof(InvernaderoJson), "invernadero")]
public partial class GreenHouseView : ContentPage
{
    private InvernaderoModel _invernadero;

    public string InvernaderoJson
    {
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _invernadero = JsonSerializer.Deserialize<InvernaderoModel>(value);
                BindingContext = new VMGreenHouseView(_invernadero);
            }
        }
    }

 //   public GreenHouseView(InvernaderoModel invernadero)
	//{
	//	InitializeComponent();
 //       BindingContext = new VMGreenHouseView(invernadero);
 //   }

    public GreenHouseView()
    {
        InitializeComponent();
        //BindingContext = new VMGreenHouseView(null);
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