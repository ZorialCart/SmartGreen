using SmartGreen.Clases;
using SmartGreen.Model;
using SmartGreen.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartGreen.ViewModel
{
    public class VMGreenHouseView : BaseViewModel
    {
        private bool _isInitialized = false;
        bool actualState;
        private InvernaderoModel invernadero;
        private bool _started;
        private string _estadoFlujo;
        private double _humedad;
        public string _nombreInvernadero;
        private double _temperatura;
        private string _idInvernadero;
        private int _minhumedad;
        private int _maxhumedad;
        private int _mintemperatura;
        private int _maxtemperatura;
        private ChangeInverStatusModel actualInverParameters;
        public string HumedadPorcentaje { get; set; } // Ejemplo del porcentaje
        public IDrawable CircularProgressDrawable { get; set; } = new CircularProgressDrawable(1f);
        private readonly InverStatusModel _invernadero;
        private readonly SignalRService _signalRService;

        public VMGreenHouseView(string id, string inverNombre, bool started)
        {
            _signalRService = new SignalRService();
            _idInvernadero = id;
            NombreInvernadero = inverNombre;
            EstadoFlujo = started ? "Apagar riego" : "Iniciar riego";
            _started = started;
            actualState = started;
        }

        public InvernaderoModel Invernadero
        {
            get => invernadero;
            set { SetValue(ref invernadero, value); }
        }

        public int minHumedad
        {
            get { return _minhumedad; }
            set { SetValue(ref _minhumedad, value); }
        }
        public int maxHumedad
        {
            get { return _maxhumedad; }
            set { SetValue(ref _maxhumedad, value); }
        }
        public int minTemperatura
        {
            get { return _mintemperatura; }
            set { SetValue(ref _mintemperatura, value); }
        }
        public int maxTemperatura
        {
            get { return _maxtemperatura; }
            set { SetValue(ref _maxtemperatura, value); }
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
            set {
                if (_isInitialized) // Solo ejecuta si la vista ya cargó
                {
                    _ = TurnOnOrTurnOfInvernadero();
                }
                SetValue(ref _started, value); }
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

        public async Task TurnOnOrTurnOfInvernadero()
        {
            bool newvalue = !_started;
            string url = $"https://5r1v7n94-5062.usw3.devtunnels.ms/api/Invernadero/ToggleStatus/{_idInvernadero}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PatchAsync(url, null);
                    if (response.IsSuccessStatusCode)
                    {
                        _started = newvalue;
                    }
                    else
                    {
                        _started = actualState;
                        if (Started)
                        {
                            await DisplayAlert("Error", $"No se pudo detener el riego. Error: {response.StatusCode}", "OK");
                        }
                        else
                        {
                            await DisplayAlert("Error", $"No se pudo iniciar el riego. Error: {response.StatusCode}", "OK");
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _started = actualState;
                await DisplayAlert("Error", $"No se pudo acceder al servicio. Error: {ex.Message}", "OK");
            }

        }

        public async Task GetInverAtributes()
        {
            using (var cliente = new HttpClient())
            {
                //string url = $"https://934vm7pw-5062.usw3.devtunnels.ms/api/Invernadero/Find/{_idInvernadero}";
                string url = $"https://5r1v7n94-5062.usw3.devtunnels.ms/api/Invernadero/Find/{_idInvernadero}";
                try
                {
                    var result = await cliente.GetAsync(url);

                    string json = await result.Content.ReadAsStringAsync();
                    Console.WriteLine($"Respuesta de la API: {json}");

                    if (result.IsSuccessStatusCode)
                    {
                        var inver = JsonSerializer.Deserialize<InvernaderoModel>(json,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (inver != null)
                        {
                            invernadero = inver;
                            maxHumedad = Invernadero.maxHumedad;
                            minHumedad = Invernadero.minHumedad;
                            maxTemperatura = Invernadero.maxTemperatura;
                            minTemperatura = Invernadero.minTemperatura;
                            actualInverParameters = new ChangeInverStatusModel
                            {
                                idInvernadero = inver.idInvernadero,
                                MaxHumedad = inver.maxHumedad,
                                MinHumedad = inver.minHumedad,
                                MaxTemperatura = inver.maxTemperatura,
                                MinTemperatura = inver.minTemperatura
                            };
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error en la respuesta: {result.StatusCode}");
                    }
                }
                catch(Exception ex)
                {

                }

            }
        }

        public async Task ChangeInverStatus() 
        {
            ChangeInverStatusModel newParameters = new ChangeInverStatusModel
            {
                idInvernadero = _idInvernadero,
                MaxHumedad = maxHumedad,
                MinHumedad = minHumedad,
                MaxTemperatura = maxTemperatura,
                MinTemperatura = minTemperatura
            };
            string url = "https://5r1v7n94-5062.usw3.devtunnels.ms/api/Invernadero/ChangeInverParameters";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonSerializer.Serialize(newParameters);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PatchAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "Se establecieron los nuevos parámetros.", "OK");
                    }
                    else
                    {
                        if (actualInverParameters != null) 
                        {
                            maxHumedad = actualInverParameters.MaxHumedad;
                            minHumedad = actualInverParameters.MinHumedad;
                            maxTemperatura = actualInverParameters.MaxTemperatura;
                            minTemperatura = actualInverParameters.MinTemperatura;
                        }
                        
                        string error = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Error", $"No se pudieron actualizar los parámetros. Error: {response.StatusCode}", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Algo salió mal. Error: {ex.Message}", "OK");
            }

        }

        public async Task GetLastStatus()
        {
            //string url = $"https://934vm7pw-5062.usw3.devtunnels.ms/GetLastStatus/{_idInvernadero}";
            string url = $"https://5r1v7n94-5062.usw3.devtunnels.ms/GetLastStatus/{_idInvernadero}";

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

                        if (lastStatus != null) 
                        {
                            Humedad = lastStatus.currentHumedad;
                            Temperatura = lastStatus.currentTemperatura;
                        }

                        
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
        public async void Toggled(bool newvalue)
        {
            if (!_isInitialized) return;
            _started = newvalue;
            //await TurnOnOrTurnOfInvernadero();
            EstadoFlujo = _started ? "Apagar riego" : "Iniciar riego";
        }

        public void Initialize()
        {
            _isInitialized = true; // Activa la ejecución solo después de la carga
        }

        public async Task InitStatus()
        {
            await _signalRService.ConnectAsync(_idInvernadero);

            _signalRService.OnStatusReceived -= UpdateStatus;
            _signalRService.OnStatusReceived += UpdateStatus;
        }

        private void UpdateStatus(InverStatusModel status)
        {
            if (status != null) 
            {
                Humedad = status.currentHumedad;
                Temperatura = status.currentTemperatura;
            }         
        }

        public async Task InitializeAsync()
        {
            await GetLastStatus();
            await InitStatus();
            await GetInverAtributes();
        }

        public async Task DisconnectAsyncSignalR()
        {
            await _signalRService.DisconnectAsync();
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
        public ICommand ChangeInverStatusCommand => new Command(async () => await ChangeInverStatus());
        //public ICommand EncenderFlujoCommand => new Command(TurnOnWaterFlow);
    }
}
