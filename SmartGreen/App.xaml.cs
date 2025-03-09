using SmartGreen.View;
using SmartGreen.View.RecoveryPass;

namespace SmartGreen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
