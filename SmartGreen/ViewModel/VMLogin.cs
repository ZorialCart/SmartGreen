using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartGreen.Model;
using SmartGreen.View;
using SmartGreen.View.RecoveryPass;
using SmartGreen.View.ViveroView;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SmartGreen.ViewModel
{
    public class VMLogin : BaseViewModel
    {         
        public VMLogin()
        {
        }

        #region VARIABLES
        private string _userName;
        private string _password;
        private string _msgUser;
        private string _msgPass;
        private bool _isLoggedIn;
        bool _sumit;

        public string Username
        {
            get { return _userName; }
            set
            {

                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string MsgUser
        {
            get { return _msgUser; }
            set
            {
                if (_msgUser != value)
                {
                    _msgUser = value;
                    OnPropertyChanged(nameof(MsgUser));
                }
            }
        }

        public string MsgPass
        {
            get { return _msgPass; }
            set
            {
                if (_msgPass != value)
                {
                    _msgPass = value;
                    OnPropertyChanged(nameof(MsgPass));
                }
            }
        }
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        public bool Sumit
        {
            get => _sumit;
            set { SetValue(ref _sumit, value); }
        }

        #endregion

        #region METHODS

        public void Validation()
        {
            MsgUser = string.Empty;
            MsgPass = string.Empty;

            bool isValid = true;

            

            if (string.IsNullOrWhiteSpace(Password))
            {
                MsgPass = "El campo no debe estar vacío.";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Username) || !Username.Contains("@"))
            {
                MsgUser = "El correo electrónico es inválido.";
                isValid = false;
            }


            Sumit = isValid;
        }

        public async void Login()
        {
            Validation();

            if (!Sumit) return;

            LoginModel requestLogin = new()
            {
                Email = Username,
                Password = Password
            };

            string url = $"https://934vm7pw-5062.usw3.devtunnels.ms/api/User/Login";



            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var response = await client.PostAsJsonAsync(url, requestLogin); // POST sin cuerpo

                    if (response.IsSuccessStatusCode)
                    {
                        string token = await response.Content.ReadAsStringAsync();
                        await SecureStorage.SetAsync("token", token);
                        IsLoggedIn = true;
                        await Shell.Current.GoToAsync("//MenuView", true);
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        MsgUser = "Correo o contraseña inválida. Intente nuevamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexión: " + ex.Message);
            }
        }


        private async void LogOut()
        {
            try
            {
                await SecureStorage.SetAsync("token", string.Empty);
                IsLoggedIn = false;
                //await Shell.Current.GoToAsync("MenuView"); //Limpia la pila de navegacion
                //await Shell.Current.Navigation.PopToRootAsync();

                await Shell.Current.GoToAsync("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar sesión: " + ex.Message);
            }

        }

        public async Task SingUp()
        {
            await Shell.Current.GoToAsync($"/{nameof(Register)}");
        }

        //public async Task GoTo()
        //{
        //    await Shell.Current.GoToAsync($"/{nameof(MenuView)}");
        //}

        public async Task RecoveryP()
        {
            await Shell.Current.GoToAsync($"/{nameof(Recovery1)}");
        }
        #endregion

        #region COMMANDS
        //public ICommand ToMenu => new Command(async () => await Login());
        public ICommand LoginCommand => new Command(() =>  Login());
        public ICommand ToSingUp => new Command(async () => await SingUp());
        public ICommand ToRecovery => new Command(async () => await RecoveryP());

        public ICommand LogOutCommand => new Command(() => LogOut());


        #endregion

    }
}
