using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartGreen.Model;

namespace SmartGreen.ViewModel
{
    internal class VMMenuView : BaseViewModel
    {
        public ObservableCollection<ModelViveros> Invernaderos { get; set; }
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
