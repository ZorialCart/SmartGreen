using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartGreen.Model;
using SmartGreen.View.ViveroView;
using SmartGreen.Clases;
using System.Web;

namespace SmartGreen.ViewModel
{
    internal class VMMenuView : BaseViewModel
    {
        public ICommand OpenFlyoutCommand { get; }
        public VMMenuView()
        {
            OpenFlyoutCommand = new Command(OpenFlyout);

            Invernaderos = new ObservableCollection<ModelViveros>();
            _ = CargarDatosAsync();
        }


        private void OpenFlyout()
        {
            // Abre el menú del Flyout
            Shell.Current.FlyoutIsPresented = true;
        }

        public async Task ToRegisterInv()
        {
            await Shell.Current.Navigation.PushAsync(new RegistrarInvernadero());
        }

        public async Task GoToInverStatus(string idInvernadero,string nombre, bool started)
        {
            await Shell.Current.GoToAsync($"GreenHouse?idInvernadero={HttpUtility.UrlEncode(idInvernadero)}&nombre={HttpUtility.UrlEncode(nombre)}&started={started}");
        }


        #region COMMANDS

        public ICommand GotToInv => new Command(async () => await ToRegisterInv());
        public ICommand GoToInverStatusCommand => new Command<ModelViveros>(async (invernadero) =>
        {
            var id = invernadero.idInvernadero;
            var nombre = invernadero.nombreInvernadero;
            var tipo = invernadero.TipoInvernadero;
            var humedadMin = invernadero.MinHumedad;
            var temperaturaMax = invernadero.MaxTemperatura;
            
            await GoToInverStatus(invernadero.idInvernadero!, invernadero.nombreInvernadero, invernadero.Started);
        });
        #endregion




        private string _nombre;
        private string _idInvernadero;
        private string _UsuCorreo;
        private string _nombreInvernadero;
        private int _tipoInvernadero;
        private double _minHumedad;
        private double _maxHumedad;
        private double _minTemperatura;
        private double _MaxTemperatura;
        private bool _started;



        // Llamado desde el constructor
        private async Task CargarDatosAsync()
        {
            await FindInvernaderos();
        }

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public string idInvernadero
        {
            get => _idInvernadero;
            set
            {
                if (_idInvernadero != value)
                {
                    _idInvernadero = value;
                    OnPropertyChanged(nameof(idInvernadero));
                }
            }
        }

        public string UsuCorreo
        {
            get => _UsuCorreo;
            set
            {
                if (_UsuCorreo != value)
                {
                    _UsuCorreo = value;
                    OnPropertyChanged(nameof(UsuCorreo));
                }
            }
        }

        public string nombreInvernadero
        {
            get => _nombreInvernadero;
            set
            {
                if (_nombreInvernadero != value)
                {
                    _nombreInvernadero = value;
                    OnPropertyChanged(nameof(nombreInvernadero));
                }
            }
        }

        public int TipoInvernadero
        {
            get => _tipoInvernadero;
            set
            {
                if (_tipoInvernadero != value)
                {
                    _tipoInvernadero = value;
                    OnPropertyChanged(nameof(TipoInvernadero));
                }
            }
        }

        public double MinHumedad
        {
            get => _minHumedad;
            set
            {
                if (_minHumedad != value)
                {
                    _minHumedad = value;
                    OnPropertyChanged(nameof(MinHumedad));
                }
            }
        }

        public double MaxHumedad
        {
            get => _maxHumedad;
            set
            {
                if (_maxHumedad != value)
                {
                    _maxHumedad = value;
                    OnPropertyChanged(nameof(MaxHumedad));
                }
            }
        }

        public double MinTemperatura
        {
            get => _minTemperatura;
            set
            {
                if (_minTemperatura != value)
                {
                    _minTemperatura = value;
                    OnPropertyChanged(nameof(MinTemperatura));
                }
            }
        }

        public double MaxTemperatura
        {
            get => _MaxTemperatura;
            set
            {
                if (_MaxTemperatura != value)
                {
                    _MaxTemperatura = value;
                    OnPropertyChanged(nameof(MaxTemperatura));
                }
            }
        }

        public bool Start
        {
            get => _started;
            set
            {
                if (_started != value)
                {
                    _started = value;
                    OnPropertyChanged(nameof(Start));
                }
            }
        }

        public ObservableCollection<ModelViveros> Invernaderos { get; set; }

        public async Task FindInvernaderos()
        {
            string? correo = await AuthService.GetUserEmailAsync();
            using (var cliente = new HttpClient())
            {
                try
                {
                    //string url = $"https://934vm7pw-5062.usw3.devtunnels.ms/api/Invernadero/FindByEmail/{correo}";
                    string url = $"http://172.16.30.247:5062/api/Invernadero/FindByEmail/{correo}";
                    var respuesta = await cliente.GetAsync(url);

                    string json = await respuesta.Content.ReadAsStringAsync();
                    Console.WriteLine($"Respuesta de la API: {json}");

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var lista = JsonSerializer.Deserialize<List<ModelViveros>>(json,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (lista != null)
                        {
                            Invernaderos.Clear();
                            foreach (var item in lista)
                            {
                                Invernaderos.Add(item);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error en la respuesta: {respuesta.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener invernaderos: {ex.Message}");
                }
            }
        }

    }
}