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

    public GreenHouseView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!_isBindingSet)
        {
            BindingContext = new VMGreenHouseView(IdInvernadero, NombreInvernadero, Started);
            _isBindingSet = true;
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