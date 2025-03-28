using SmartGreen.ViewModel;
namespace SmartGreen.View.ViveroView;

public partial class MenuView : ContentPage
{
	public MenuView()
	{
		InitializeComponent();
        // VMMenuView vMMenuView = new VMMenuView();
        // BindingContext = vMMenuView;
        BindingContext = new VMMenuView();

    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        

    }
    private async void AddGreenHouse_Clicked(object sender, EventArgs e)
    {


    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Aquí puedes llamar a la función que recarga los datos de los invernaderos
        var vmMenu = BindingContext as VMMenuView;

        if (vmMenu != null)
        {
            await vmMenu.FindInvernaderos(); // Llamamos al método para cargar los invernaderos
        }
    }
}