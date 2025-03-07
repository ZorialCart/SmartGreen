using SmartGreen.ViewModel;

namespace SmartGreen.View.RecoveryPass;

public partial class Recovery1 : ContentPage
{
	public Recovery1()
	{
		InitializeComponent();
		VMRecovery1 vMRecovery1 = new VMRecovery1();
		BindingContext = vMRecovery1;
	}
}