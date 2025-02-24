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

namespace SmartGreen.ViewModel
{
    internal class VMLogin : BaseViewModel
    {
        public VMLogin() { 
        }

        #region VARIABLES
        private string _userName;
        private string _password;
        private string _msgUser;
        private string _msgPass;
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

        public bool Sumit
        {
            get => _sumit;
            set { SetValue(ref _sumit, value); }
        }

        #endregion

        public enum EmailValidationResult
        {
            Success,       // El correo existe
            NotFound,      // El correo no existe
            ServerError,   // Error en el servidor
            InvalidRequest // El correo tiene un formato inválido
        }

        public async Task<EmailValidationResult> FindByEmail(string correo)
        {

            using (var cliente = new HttpClient())
            {
                try

                {
                    string url = $"https://934vm7pw-5062.usw3.devtunnels.ms/api/User/Correo?correo={correo}";

                    var respuesta = await cliente.GetAsync(url);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string responseContent = await respuesta.Content.ReadAsStringAsync();
                        return string.IsNullOrEmpty(responseContent) ? EmailValidationResult.NotFound : EmailValidationResult.Success;
                    }
                    else
                    {
                        // Si el código de estado es 404, significa que el correo no se encuentra
                        if (respuesta.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            Console.WriteLine($"El correo no existe.");
                            return EmailValidationResult.NotFound;
                        }
                        else
                        {
                            Console.WriteLine($"Error en la respuesta: {respuesta.StatusCode}");
                            return EmailValidationResult.ServerError;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al hacer la solicitud: {ex.Message}");
                    return EmailValidationResult.ServerError;
                }
            }

        }

        public async Task Validation()
        {
            MsgUser = string.Empty;
            MsgPass = string.Empty;

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Username) || !Username.Contains("@"))
            {
                MsgUser = "El correo electrónico es inválido.";
                isValid = false;
            }
   

            if (string.IsNullOrWhiteSpace(Password))
            {
                MsgPass = "El campo no debe estar vacío.";
                isValid = false;
            }

            EmailValidationResult resp = await FindByEmail(Username);
            if (resp == EmailValidationResult.NotFound)
            {
                MsgUser = "El correo no existe, intente nuevamente.";
                isValid = false;
            }
            else if (resp == EmailValidationResult.ServerError)
            {
                MsgUser = "Hubo un error en el servidor. Por favor, intente más tarde.";
                isValid = false;
            }

            Sumit = isValid;
        }

        public async Task Login(UserModel userModel)
        {


            await Validation(); 

            if (!Sumit) return;

            userModel.Correo = Username.Trim();
            userModel.Password = Password.Trim();

            using (var cliente = new HttpClient())
            {
                try
                {
                    string json = JsonSerializer.Serialize(userModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var respuesta = await cliente.PostAsync("https://934vm7pw-5062.usw3.devtunnels.ms/api/User/Login", content);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        await GoTo();
                    }
                    else
                    {
                        string errorResponse = await respuesta.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error en la respuesta: {respuesta.StatusCode}, {errorResponse}");

                        if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            MsgUser = "Correo o contraseña incorrectos. Por favor, intente nuevamente.";
                        }
                        else
                        {
                            MsgUser = "Hubo un error al realizar el login. Intente más tarde.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la solicitud: {ex.Message}");
                    MsgUser = "Hubo un error al intentar conectar con el servidor. Intente más tarde.";
                }
            }
        }

        public async Task Pruebaa()
        {
            await Shell.Current.GoToAsync($"/{nameof(MenuInvernaderos)}");
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
        public ICommand ToMenu => new Command(async () => await Login(new UserModel()));
        public ICommand ToSingUp => new Command(async() => await SingUp());
        public ICommand Paraprobar => new Command(async() => await Pruebaa());

        public ICommand ToRecovery => new Command(async () => await RecoveryP());




    }
}
