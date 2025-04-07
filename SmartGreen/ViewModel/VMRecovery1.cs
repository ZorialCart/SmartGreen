using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SmartGreen.Clases;
using SmartGreen.Model;
using SmartGreen.View;
using SmartGreen.View.RecoveryPass;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartGreen.ViewModel
{
    internal class VMRecovery1 : BaseViewModel
    {
        public VMRecovery1() { 
        }
        #region VARIABLES
        private string _correo;
        private string _msgCorreo;
        private bool _sumit;

        public string Correo
        {
            get => _correo;
            set
            {
                if(value != _correo) 
                   _correo = value;
                {
                    _correo = value;
                    OnpropertyChanged(nameof(Correo));
                }

            }
        }

        public string MsgCorreo
        {
            get => _msgCorreo;
            set
            {
                if(value != _msgCorreo)
                {
                    _msgCorreo = value;
                    OnpropertyChanged(nameof(MsgCorreo));
                }
            }
        }

        public bool Sumit
        {
            get => _sumit;
            set { SetValue(ref _sumit, value); }
        }
        #endregion


        #region METODOS

        public void Validate()
        {
            MsgCorreo = string.Empty;
            bool isValid = true;

            if(string.IsNullOrEmpty(Correo))
            {
                MsgCorreo = "El campo no debe estar vacío.";
                isValid = false;
            }

            if (!Correo.Contains("@"))
            {
                MsgCorreo = "El correo no es válido.";
                isValid = false;
            }

            Sumit = isValid;

        }

        public async void RecoveryRequest()
        {
            Validate();
            if (!Sumit) throw new InvalidOperationException("Error en los datos.");

            string url = "https://934vm7pw-5062.usw3.devtunnels.ms/api/Recovery/coderequest";
            var email = Correo;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestModel = new SendEmail { Email = Correo };

                    var jsonContent = JsonConvert.SerializeObject(requestModel);
                    var content = new StringContent($"\"{email}\"", Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        await AuthService.SaveEmail(email);
                        await DisplayAlert("Exito", "Correo enviado", "Aceptar");
                        await Shell.Current.GoToAsync($"/{nameof(RecoveryCode)}");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Error", $"No se pudo enviar el correo. Detalles: {errorContent}", "Aceptar");
                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo enviar el correo: {ex.Message}", "Aceptar");
            }
        }


        #endregion

        public async Task ReturnToLogin()
        {
            //await Shell.Current.GoToAsync($"/{nameof(Login)}");
            await Shell.Current.Navigation.PopAsync();
        }

        //public async Task ToRecovery2()
        //{
        //    await RecoveryRequest(Correo);
        //}

        //Comandos
        public ICommand ReturnLogIn => new Command(async () => await ReturnToLogin());
        public ICommand ToRecov2 => new Command(() =>  RecoveryRequest());

    }
}

