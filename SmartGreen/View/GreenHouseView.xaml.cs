using SmartGreen.Model;
using SmartGreen.ViewModel;
using System.Text.Json;

namespace SmartGreen.View;

[QueryProperty(nameof(IdInvernadero), "idInvernadero")]
[QueryProperty(nameof(NombreInvernadero), "nombre")]
[QueryProperty(nameof(Started), "started")]
public partial class GreenHouseView : ContentPage
{
    public string IdInvernadero { get; set; }
    public string NombreInvernadero { get; set; }
    public bool Started { get; set; }

    private bool _isBindingSet = false;

    VMGreenHouseView VMGreenHouseview; 

    public GreenHouseView()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_isBindingSet)
        {
            VMGreenHouseview = new VMGreenHouseView(IdInvernadero, NombreInvernadero, Started);
            BindingContext = VMGreenHouseview;
            _isBindingSet = true;

            await VMGreenHouseview.InitializeAsync();
            (BindingContext as VMGreenHouseView)?.Initialize();
        }
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        if (VMGreenHouseview != null)
        {
            await VMGreenHouseview.DisconnectAsyncSignalR();
        }
    }

    //   public GreenHouseView(InvernaderoModel invernadero)
    //{
    //	InitializeComponent();
    //       BindingContext = new VMGreenHouseView(invernadero);
    //   }


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