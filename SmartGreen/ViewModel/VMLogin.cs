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
    internal class VMLogin : BaseViewModel
    {
        public VMLogin() { 
           
        
        }

        public async Task GoTo()
        {
            await Shell.Current.GoToAsync($"/{nameof(MenuView)}");
        }
        //Comandos
        public ICommand ToMenu => new Command (async() => await GoTo());

       
    }
}
