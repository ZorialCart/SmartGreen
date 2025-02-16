using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartGreen.View;
using SmartGreen.View.ViveroView;

namespace SmartGreen.ViewModel
{
    internal class VMRegister : BaseViewModel
    {
        public VMRegister() { 
        }

        public async Task BackToLogin()
        {
            await Shell.Current.GoToAsync($"/{nameof(Login)}");
        }

        //Comandos

        public ICommand ReturnToLogin => new Command ( async () => await BackToLogin());

    }
}
