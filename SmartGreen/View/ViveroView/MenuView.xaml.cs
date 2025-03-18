using SmartGreen.ViewModel;
namespace SmartGreen.View.ViveroView;

public partial class MenuView : ContentPage
{
	public MenuView()
	{
		InitializeComponent();
        VMMenuView vMMenuView = new VMMenuView(Navigation);
        BindingContext = vMMenuView;

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

}