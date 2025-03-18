using SmartGreen.View;
using SmartGreen.View.RecoveryPass;
using Microsoft.Maui.Controls;
using SmartGreen.Clases;
using SmartGreen.View.ViveroView;
using System.Threading.Tasks;

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
