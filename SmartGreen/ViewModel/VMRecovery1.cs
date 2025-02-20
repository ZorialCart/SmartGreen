using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartGreen.View;
using SmartGreen.View.RecoveryPass;

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

        public async Task ToRecovery2()
        {
            await Shell.Current.GoToAsync($"/{nameof(Recovery2)}");
        }

        //Comandos
        public ICommand ReturnLogIn => new Command(async () => await ReturnToLogin());
        public ICommand ToRecov2 => new Command(async () => await ToRecovery2());

    }
}

