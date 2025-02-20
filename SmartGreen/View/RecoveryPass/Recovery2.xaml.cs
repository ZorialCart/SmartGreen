using SmartGreen.ViewModel;
namespace SmartGreen.View.RecoveryPass;

public partial class Recovery2 : ContentPage
{
	public Recovery2()
    {
		InitializeComponent();
        VMRecovery2 vMRecovery2 = new VMRecovery2();
        BindingContext = vMRecovery2;
    }
}