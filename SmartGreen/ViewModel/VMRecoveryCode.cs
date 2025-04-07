using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartGreen.Clases;
using SmartGreen.Model;
using SmartGreen.View.RecoveryPass;

namespace SmartGreen.ViewModel
{
    public class VMRecoveryCode : BaseViewModel
    {
        public VMRecoveryCode()
        {
         
        }

        #region VARIABLES
        private string _code;
        private string _msgCode;
        private bool _sumit;

        public string Code
        {
            get => _code;
            set
            {
                if (value != _code)
                    _code = value;
                {
                    _code = value;
                    OnpropertyChanged(nameof(Code));
                }
            }
        }
        public string MsgCode
        {
            get => _msgCode;
            set
            {
                if (value != _msgCode)
                {
                    _msgCode = value;
                    OnpropertyChanged(nameof(MsgCode));
                }
            }
        }
        public bool Sumit
        {
            get => _sumit;
            set { SetValue(ref _sumit, value); }
        }
        #endregion

        #region METHODS

        public void Validate()
        {
            MsgCode = string.Empty;
            bool isValid = true;

            if (string.IsNullOrEmpty(Code))
            {
                MsgCode = "El campo no puede estar vacio";
                isValid = false;
            }
            if(Code.Length != 6)
            {
                MsgCode = "El código debe tener 6 caracteres.";
                isValid = false;
            }

            Sumit = isValid;

        }

        public async void SendCode()
        {
            Validate();
            if(!Sumit) throw new InvalidOperationException("El código es inválido.");

            string url = "https://934vm7pw-5062.usw3.devtunnels.ms/api/Recovery/validatecode";
            var email = await AuthService.GetUserEmailAsync();

            try
            {
                using(HttpClient client = new HttpClient())
                {
                    var requestModel = new  { Code, email };
                    var json = JsonConvert.SerializeObject(requestModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        await AuthService.SaveCodeAsync(Code);
                        await DisplayAlert("Mensaje", "El código es correcto.", "Ok");
                        await Shell.Current.Navigation.PushAsync(new Recovery2());
                    }
                    else
                    {
                        MsgCode = "El código no es válido.";
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo enviar el código: {ex.Message}", "Aceptar");
            }
        }

        public async Task Back()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        #endregion

        #region COMMANDS

        public Command SendCodeCommand => new Command(SendCode);
        public Command ToBack => new Command(async () => await Back());
        #endregion
    }
}
