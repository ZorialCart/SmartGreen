using SmartGreen.ViewModel;
namespace SmartGreen.View.RecoveryPass;

public partial class RecoveryCode : ContentPage
{
	public RecoveryCode()
	{
		InitializeComponent();
        VMRecoveryCode vMRecoveryCode = new VMRecoveryCode();
        BindingContext = vMRecoveryCode;
    }
}