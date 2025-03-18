using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SmartGreen.Model;
using SmartGreen.View;
using SmartGreen.View.ViveroView;

namespace SmartGreen.ViewModel
{
    public class VMRegistrarInvernaderos : BaseViewModel
    {
        #region VARIABLES

        private string _idInvernadero;
        private string _nombreInvernadero;
        private string _descripcion;
        private string _msgId;
        private string _msgNombre;
        private string _msgDescripcion;
        private bool _idDisponible = true;
        private bool _sumit;
        private readonly string correo = "alex@gmail.com";  // Simulación de usuario autenticado
        private readonly HttpClient _cliente = new HttpClient();
        private readonly string apiUrl = "https://934vm7pw-5062.usw3.devtunnels.ms/api/Invernadero";

        #endregion

        #region PROPIEDADES

        public string IdInvernadero
        {
            get => _idInvernadero;
            set
            {
                if (_idInvernadero != value)
                {
                    _idInvernadero = value;
                    OnPropertyChanged(nameof(IdInvernadero));
                    ValidarIdInvernadero();
                }
            }
        }

        public string NombreInvernadero
        {
            get => _nombreInvernadero;
            set
            {
                if (_nombreInvernadero != value)
                {
                    _nombreInvernadero = value;
                    OnPropertyChanged(nameof(NombreInvernadero));
                }
            }
        }

        public string Descripcion
        {
            get => _descripcion;
            set
            {
                if (_descripcion != value)
                {
                    _descripcion = value;
                    OnPropertyChanged(nameof(Descripcion));
                }
            }
        }

        public string MsgId
        {
            get => _msgId;
            set
            {
                if (_msgId != value)
                {
                    _msgId = value;
                    OnPropertyChanged(nameof(MsgId));
                }
            }
        }

        public string MsgNombre
        {
            get => _msgNombre;
            set
            {
                if (_msgNombre != value)
                {
                    _msgNombre = value;
                    OnPropertyChanged(nameof(MsgNombre));
                }
            }
        }

        public string MsgDescripcion
        {
            get => _msgDescripcion;
            set
            {
                if (_msgDescripcion != value)
                {
                    _msgDescripcion = value;
                    OnPropertyChanged(nameof(MsgDescripcion));
                }
            }
        }

        public bool IdDisponible
        {
            get => _idDisponible;
            set
            {
                SetProperty(ref _idDisponible, value);
                OnPropertyChanged(nameof(IdDisponible));
            }
        }

        public bool Sumit
        {
            get => _sumit;
            set
            {
                SetProperty(ref _sumit, value);
                OnPropertyChanged(nameof(Sumit));
            }
        }

        #endregion

        #region COMANDOS

        public VMRegistrarInvernaderos()
        {

        }

        #endregion

        #region MÉTODOS

        private async void ValidarIdInvernadero()
        {
            MsgId = string.Empty;
            if (string.IsNullOrWhiteSpace(IdInvernadero))
            {
                MsgId = "El ID del invernadero no puede estar vacío.";
                IdDisponible = false;
                return;
            }

            try
            {
                // Verificar si el invernadero ya tiene un correo registrado
                var response = await _cliente.GetAsync($"{apiUrl}/FindByEmail/{correo}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var invernaderosUsuario = JsonConvert.DeserializeObject<List<InvernaderoModel>>(jsonResponse);

                    // Buscar si el ID ingresado ya está asignado a alguien
                    var invernaderoExistente = invernaderosUsuario?.Find(i => i.idInvernadero == IdInvernadero);

                    if (invernaderoExistente != null)
                    {
                        MsgId = "Este invernadero ya está registrado por otro usuario.";
                        IdDisponible = false;
                    }
                    else
                    {
                        IdDisponible = true;
                    }
                }
                else
                {
                    MsgId = "Error al verificar el ID. Inténtalo de nuevo.";
                    IdDisponible = false;
                }
            }
            catch (Exception ex)
            {
                MsgId = $"Error en la solicitud: {ex.Message}";
                IdDisponible = false;
            }
        }

        public void ValidarCampos()
        {
            MsgId = string.Empty;
            MsgNombre = string.Empty;
            MsgDescripcion = string.Empty;
            Sumit = true;

            if (string.IsNullOrWhiteSpace(IdInvernadero))
            {
                MsgId = "El ID del invernadero no puede estar vacío.";
                Sumit = false;
            }
            else if (!IdDisponible)
            {
                MsgId = "Este invernadero ya está registrado por otro usuario.";
                Sumit = false;
            }

            if (string.IsNullOrWhiteSpace(NombreInvernadero))
            {
                MsgNombre = "El nombre del invernadero no puede estar vacío.";
                Sumit = false;
            }

            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                MsgDescripcion = "La descripción no puede estar vacía.";
                Sumit = false;
            }
        }

        public async Task RegistrarInvernadero()
        {
            ValidarCampos();
            if (!Sumit) return;

            var inverModel = new InvernaderoModel
            {
                idInvernadero = IdInvernadero.Trim(),
                nombreInvernadero = NombreInvernadero.Trim(),
                usuCorreo = correo,
                descripcion = Descripcion.Trim(),
                tipoInvernadero = 1
            };

            try
            {
                string json = JsonConvert.SerializeObject(inverModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var respuesta = await _cliente.PutAsync($"{apiUrl}/RegistrarInvernadero", content);

                if (respuesta.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Invernadero registrado correctamente", "OK");
                    await BackToMenu();
                }
                else
                {
                    string errorMsg = await respuesta.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo registrar: {errorMsg}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error en la solicitud: {ex.Message}", "OK");
            }
        }

        public async Task Volver()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task BackToMenu()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        #endregion

        #region Comandos
        public ICommand RegistrarInvernaderoCommand => new Command(async () => await RegistrarInvernadero());
        public ICommand VolverCommand => new Command(async () => await Volver());
        #endregion
    }
}