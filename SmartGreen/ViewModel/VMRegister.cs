using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartGreen.Model;
using SmartGreen.View;
using SmartGreen.View.ViveroView;

namespace SmartGreen.ViewModel
{
    internal class VMRegister : BaseViewModel
    {
        public VMRegister()
        {
        }
        #region VARIABLES
        private string _nombre;
        private string _correo;
        private string _celular;
        private string _password;
        private string _usuarioTipo;
        private string _passMistake;
        private string _confirmPass;
        private string _confirmPassMistake;
        private string _msgName;
        private string _msgEmail;
        private string _msgCelular;
        bool sumit;


        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnpropertyChanged(nameof(Nombre));
                }
            }
        }

        public string Correo
        {
            get => _correo;
            set
            {
                if (_correo != value)
                {
                    _correo = value;
                    OnpropertyChanged(nameof(Correo));
                }
            }
        }
        public string Celular
        {
            get => _celular;
            set
            {
                if (_celular != value)
                {
                    _celular = value;
                    OnpropertyChanged(nameof(Celular));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnpropertyChanged(nameof(Password));
                }
            }

        }

        public string UsuarioTipo
        {
            get => _usuarioTipo;
            set { SetValue(ref _usuarioTipo, value); }

        }
        public string ConfirmPass
        {
            get => _confirmPass;
            set
            {
                if (_confirmPass != value)
                {
                    _confirmPass = value;
                    OnpropertyChanged(nameof(ConfirmPass));
                }
            }
        }

        public string PassMistake
        {
            get => _passMistake;
            set { SetValue(ref _passMistake, value); }
        }

        public string ConfirmPassMistake
        {
            get => _confirmPassMistake;
            set { SetValue(ref _confirmPassMistake, value); }
        }

        public string MsgNombre
        {
            get => _msgName;
            set { SetValue(ref _msgName, value); }
        }

        public string MsgEmail
        {
            get => _msgEmail;
            set { SetValue(ref _msgEmail, value); }
        }

        public string MsgCelular
        {
            get => _msgCelular;
            set { SetValue(ref _msgCelular, value); }
        }

        public bool Sumit
        {
            get => sumit;
            set { SetValue(ref sumit, value); }
        }

        #endregion

        public async Task<bool> FindByEmail(string correo)
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
                        return !string.IsNullOrEmpty(responseContent); // Respuesta para cuando el correo existe
                    }
                    else
                    {
                        Console.WriteLine($"Error en la respuesta: {respuesta.StatusCode}");
                        return false; // Respuesta por si el correo no esta registrado
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al hacer la solicitud: {ex.Message}");
                    return false;
                }

            }

        }

        public async void Validate()
        {
            MsgNombre = string.Empty;
            MsgCelular = string.Empty;
            MsgEmail = string.Empty;
            PassMistake = string.Empty;
            ConfirmPassMistake = string.Empty;

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Nombre))
            {
                MsgNombre = "El campo no debe estar vacío.";
                isValid = false;
            }
            bool resp = await FindByEmail(Correo);
            if (string.IsNullOrWhiteSpace(Correo) || !Correo.Contains("@"))
            {
                MsgEmail = "El correo electrónico es inválido.";
                isValid = false;
            }
            else
            {
                if (resp)
                {
                    MsgEmail = "El correo ya existe, intente nuevamente.";
                    isValid = false;
                }
            }

            if (string.IsNullOrWhiteSpace(Celular) || !Celular.All(char.IsDigit) || Celular.Length != 10)
            {
                MsgCelular = "El celular debe contener 10 números.";
                isValid = false;
            }
            else


            if (string.IsNullOrEmpty(Password) || Password.Length < 8)
            {
                PassMistake = "La contraseña debe tener al menos 8 caracteres.";
                isValid = false;
            }

            if (!Password.Any(char.IsUpper))
            {
                PassMistake = "La contraseña debe contener al menos una letra mayúscula.";
                isValid = false;
            }

            if (!Password.Any(char.IsDigit))
            {
                PassMistake = "La contraseña debe contener al menos un número.";
                isValid = false;
            }

            if (Password != ConfirmPass)
            {
                ConfirmPassMistake = "Las contraseñas no coinciden.";
                isValid = false;
            }

            Sumit = isValid;

        }

        public async Task Register(UserModel userModel)
        {
            Validate();

            if (!Sumit) return;

            userModel.Id = "";
            userModel.Nombre = Nombre.Trim();
            userModel.Correo = Correo.Trim();
            userModel.Celular = Celular.Trim();
            userModel.Password = Password.Trim();
            userModel.UsuarioTipo = "user";

            using (var cliente = new HttpClient())
            {
                try
                {
                    string json = JsonSerializer.Serialize(userModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var respuesta = await cliente.PostAsync("https://934vm7pw-5062.usw3.devtunnels.ms/api/User/Register", content);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Mensaje", "La cuenta se creo correctamente.", "Ok");
                        await BackToLogin();
                    }
                    else
                    {
                        string errorResponse = await respuesta.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error en la respuesta: {respuesta.StatusCode}, {errorResponse}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la solicitud: {ex.Message}");
                }

            }

        }

        public async Task BackToLogin()
        {
            await Shell.Current.Navigation.PopAsync();
        }


        //Comandos

        public ICommand ReturnToLogin => new Command(async () => await BackToLogin());
        public ICommand RegisterCmd => new Command(async () => await Register(new UserModel()));

    }
}