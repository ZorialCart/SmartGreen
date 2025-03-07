using SmartGreen.Clases;
using SmartGreen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartGreen.ViewModel
{
    class VMGreenHouseView : BaseViewModel
    {
        private bool _started;
        private string _estadoFlujo;
        private double _humedad;
        public string _nombreInvernadero;
        private double _temperatura;
        public string HumedadPorcentaje { get; set; } = "45%"; // Ejemplo del porcentaje
        public IDrawable CircularProgressDrawable { get; set; } = new CircularProgressDrawable(1f);
        private readonly InvernaderoModel _invernadero;

        public VMGreenHouseView(InvernaderoModel inverStatus)
        {
            _invernadero = inverStatus;
            if (_invernadero != null)
            {
                Started = _invernadero.started;
                NombreInvernadero = _invernadero.nombreInvernadero!;
                _ = GetLastStatus();
            }
            
        }

        public string EstadoFlujo
        {
            get { return _estadoFlujo; }
            set { SetValue(ref _estadoFlujo, value); }
        }

        public string NombreInvernadero
        {
            get => _nombreInvernadero;
            set { SetValue(ref _nombreInvernadero, value); }
        }
        public bool Started
        {
            get => _started;
            set { SetValue(ref _started, value); }
        }

        public double Humedad
        {
            get => _humedad;
            set { SetValue(ref _humedad, value); }
        }

        public double Temperatura
        {
            get => _temperatura;
            set { SetValue(ref _temperatura, value); }
        }

        //public VMGreenHouseView()
        //{
        //   // Navigation = navigation;
        //}

        public async Task Volver()
        {
            await Shell.Current.GoToAsync("..");
            //await Navigation.PopAsync();
        }

        public async Task GetLastStatus()
        {
            string url = $"http://192.168.1.95:5062/GetLastStatus/{_invernadero.idInvernadero}";

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var response = await client.GetAsync(url); // POST sin cuerpo

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        var lastStatus = JsonSerializer.Deserialize<InverStatusModel>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        Humedad = lastStatus.currentHumedad;
                        Temperatura = lastStatus.currentTemperatura;
                        //await GoTo();
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        //MsgUser = "Corro o contraseña inválida. Intente nuevamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexión: " + ex.Message);
            }
        }
        public void Toggled(bool newvalue)
        {
            Started = newvalue;
            if (Started)
            {
                EstadoFlujo = "Apagar riego";
                return;
            }
            EstadoFlujo = "Iniciar riego";
        }
        //public void TurnOnWaterFlow()
        //{
        //    if (EstadoFlujo == "Iniciar riego")
        //    {
        //        EstadoFlujo = "Apagar riego";
        //        return;
        //    }
        //    EstadoFlujo = "Iniciar riego";
        //}

        public ICommand ToggledCommand => new Command<bool>(Toggled);
        public ICommand VolverCommand => new Command(async () => await Volver());
        //public ICommand EncenderFlujoCommand => new Command(TurnOnWaterFlow);
    }
}
