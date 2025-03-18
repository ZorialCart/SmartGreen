using SmartGreen.ViewModel;
namespace SmartGreen.View.ViveroView;

public partial class MenuView : ContentPage
{
	public MenuView()
	{
       // BindingContext = new VMMenuView(Navigation);
        VMMenuView vMMenuView = new VMMenuView();
        BindingContext = vMMenuView;
    }

}