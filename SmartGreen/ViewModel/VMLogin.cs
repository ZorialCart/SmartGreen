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
using Microsoft.Maui.Controls;
using SmartGreen.Clases;


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

            //string url = $"https://934vm7pw-5062.usw3.devtunnels.ms/api/User/Login";
            string url = $"http://172.16.30.247:5062/api/User/Login";



            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var response = await client.PostAsJsonAsync(url, requestLogin); 

                    if (response.IsSuccessStatusCode)
                    {
                        string token = await response.Content.ReadAsStringAsync();
                        await AuthService.SaveTokenAsync(token);
                        await AuthService.SaveEmail(Username);
                        IsLoggedIn = true;
                        if (Shell.Current.Navigation.NavigationStack.Count > 1)
                        {
                            var loginPage = Shell.Current.Navigation.NavigationStack.FirstOrDefault(p => p is Login);
                            if (loginPage != null)
                            {
                                Shell.Current.Navigation.RemovePage(loginPage);
                            }
                        }

                        await Shell.Current.GoToAsync("//MenuView");

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
                await AuthService.LogOutAsync();
                IsLoggedIn = false;

                Application.Current.MainPage = new NavigationPage(new Login());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar sesión: " + ex.Message);
            }

        }


        public async Task SingUp()
        {
            await Shell.Current.Navigation.PushAsync(new Register());
        }

        public async Task RecoveryP()
        {
            await Shell.Current.Navigation.PushAsync(new Recovery1());
        }
        #endregion

        #region COMMANDS
        public ICommand LoginCommand => new Command(() =>  Login());
        public ICommand ToSingUp => new Command(async () => await SingUp());
        public ICommand ToRecovery => new Command(async () => await RecoveryP());

        public ICommand LogOutCommand => new Command(() => LogOut());


        #endregion

    }
}
