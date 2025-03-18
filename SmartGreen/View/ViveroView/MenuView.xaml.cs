using SmartGreen.ViewModel;
namespace SmartGreen.View.ViveroView;

public partial class MenuView : ContentPage
{
	public MenuView()
	{
		InitializeComponent();
        BindingContext = new VMMenuView(Navigation);
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        
    }
    private async void AddGreenHouse_Clicked(object sender, EventArgs e)
    {

    }

}