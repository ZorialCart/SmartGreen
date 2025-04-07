using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SmartGreen.Clases;
using SmartGreen.Model;

namespace SmartGreen.ViewModel
{
    public class VMRecovery2 : BaseViewModel
    {
        public VMRecovery2() { }
        #region VARIABLES
        private string _newPass;
        private string _confirmPass;
        private string _msgPass;
        private string _msgConfirmPass;
        private bool _sumit;

        public string NewPass
        {
            get => _newPass;
            set
            {
                if (value != _newPass)
                    _newPass = value;
                {
                    _newPass = value;
                    OnpropertyChanged(nameof(NewPass));
                }
            }
        }

        public string ConfirmPass
        {
            get => _confirmPass;
            set
            {
                if (value != _confirmPass)
                    _confirmPass = value;
                {
                    _confirmPass = value;
                    OnpropertyChanged(nameof(ConfirmPass));
                }
            }
        }
        public string MsgPass
        {
            get => _msgPass;
            set
            {
                if (value != _msgPass)
                {
                    _msgPass = value;
                    OnpropertyChanged(nameof(MsgPass));
                }
            }
        }
        public string MsgConfirmPass
        {
            get => _msgConfirmPass;
            set
            {
                if (value != _msgConfirmPass)
                {
                    _msgConfirmPass = value;
                    OnpropertyChanged(nameof(MsgConfirmPass));
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
            MsgPass = string.Empty;
            MsgConfirmPass = string.Empty;
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(NewPass))
            {
                MsgPass = "Campo requerido";
                isValid = false;
            }
          
            if (string.IsNullOrWhiteSpace(ConfirmPass))
            {
                MsgConfirmPass = "Campo requerido";
                isValid = false;
            }
            else if (NewPass.Length < 8)
            {
                MsgPass = "La contraseña debe tener al menos 8 caracteres";
                isValid = false;
            }
            else if (NewPass != ConfirmPass)
            {
                MsgConfirmPass = "Las contraseñas no coinciden";
                isValid = false;
            }

            Sumit = isValid;
        }

        public async void ChangePass()
        {
            Validate();
            if (!Sumit) await DisplayAlert("Error", "Por favor, corrige los errores antes de continuar.", "OK");

            string url = "https://934vm7pw-5062.usw3.devtunnels.ms/api/Recovery/changewithcode";
            var code = await AuthService.GetCodeAsync();
            var email = await AuthService.GetUserEmailAsync();
            var changerequest = new ChangePassModel
            {
                NewPassword = NewPass,
                ConfirmPassword = ConfirmPass,
                Code = code,
                Email = email
            };

            try
            {
                using (HttpClient client = new HttpClient()) 
                {
                    var json = JsonConvert.SerializeObject(changerequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "Contraseña cambiada con éxito", "OK");
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Error", "No se pudo cambiar la contraseña: " + errorMessage, "OK");
                    }

                }
                
               
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo cambiar la contraseña: " + ex.Message, "OK");
            }
        }
        #endregion

        public ICommand Change => new Command(() => ChangePass());

    }
}
