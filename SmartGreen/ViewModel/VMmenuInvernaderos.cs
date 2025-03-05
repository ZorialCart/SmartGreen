using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using SmartGreen.Model;
using SmartGreen.View;

namespace SmartGreen.ViewModel;
public class VMmenuInvernaderos : BaseViewModel
{
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
    private readonly UserModel _user;

    // Constructor
    public VMmenuInvernaderos()
    {
        Invernaderos = new ObservableCollection<InvernaderoModel>();
        _ = CargarDatosAsync(); // Llama al método asíncrono
        
    }

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
                OnPropertyChanged(nameof(Nombre)); // <-- Corrige si tu BaseViewModel usa OnPropertyChanged
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

    public ObservableCollection<InvernaderoModel> Invernaderos { get; set; }
    string correo = "corickgonzalez123@gmail.com";
    public async Task FindInvernaderos()
    {
        using (var cliente = new HttpClient())
        {
            try
            {
                string url = $"http://192.168.1.95:5062/api/Invernadero/FindByEmail/{correo}";
                var respuesta = await cliente.GetAsync(url);

                // Debug: imprime respuesta JSON
                string json = await respuesta.Content.ReadAsStringAsync();
                //Console.WriteLine($"Respuesta de la API: {json}");

                if (respuesta.IsSuccessStatusCode)
                {
                    var lista = JsonSerializer.Deserialize<List<InvernaderoModel>>(json,
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

    public async Task GoToInverStatus(InvernaderoModel invernadero)
    {
        // Serializar el modelo a JSON y codificarlo en la URL
        string json = JsonSerializer.Serialize(invernadero);
        string encodedJson = HttpUtility.UrlEncode(json);
        await Shell.Current.GoToAsync($"/GreenHouseView?invernadero={encodedJson}");
    }

    // Método para ir a la página de registro
    public async Task GoToRegisterInvernadero()
    {
        await Shell.Current.GoToAsync($"/{nameof(AgregarInvernadero)}");
    }

    // Comando
    public ICommand GoToRegInv => new Command(async () => await GoToRegisterInvernadero());
    public ICommand GoToInverStatusCommand => new Command<InvernaderoModel>(async (invernadero) => await GoToInverStatus(invernadero));
}

