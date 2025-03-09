using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartGreen.ViewModel
{
    internal class VMMenuView : BaseViewModel
    {
        public ICommand OpenFlyoutCommand { get; }
        public VMMenuView()
        {
            OpenFlyoutCommand = new Command(OpenFlyout);
        }
        private void OpenFlyout()
        {
            // Abre el menú del Flyout
            Shell.Current.FlyoutIsPresented = true;
        }
    }
}
