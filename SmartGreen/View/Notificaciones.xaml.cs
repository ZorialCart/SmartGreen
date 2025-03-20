using SmartGreen.ViewModel;

namespace SmartGreen.View;

public partial class Notificaciones : ContentPage
{
	public Notificaciones()
	{
		InitializeComponent();
		BindingContext = new VMNotificaciones();
    }
}