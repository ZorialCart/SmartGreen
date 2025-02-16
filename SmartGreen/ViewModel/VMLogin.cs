using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartGreen.View;
using SmartGreen.View.RecoveryPass;
using SmartGreen.View.ViveroView;

namespace SmartGreen.ViewModel
{
    internal class VMLogin : BaseViewModel
    {
        public VMLogin() { 
           
        
        }

        public async Task SingUp()
        {
            await Shell.Current.GoToAsync($"/{nameof(Register)}");
        }

        public async Task GoTo()
        {
            await Shell.Current.GoToAsync($"/{nameof(MenuView)}");
        }

        public async Task RecoveryP()
        {
            await Shell.Current.GoToAsync($"/{nameof(Recovery1)}");
        }
        //Comandos
        public ICommand ToMenu => new Command (async() => await GoTo());
        public ICommand ToSingUp => new Command(async() => await SingUp());

        public ICommand ToRecovery => new Command(async () => await RecoveryP());



    }
}
