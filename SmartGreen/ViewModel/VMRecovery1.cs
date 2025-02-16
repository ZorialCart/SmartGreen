using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartGreen.View;

namespace SmartGreen.ViewModel
{
    internal class VMRecovery1
    {
        public VMRecovery1() { 
        }

        public async Task ReturnToLogin()
        {
            await Shell.Current.GoToAsync($"/{nameof(Login)}");
        }
        
        //Comandos
        public ICommand ReturnLogIn => new Command(async () => await ReturnToLogin());

    }
}
