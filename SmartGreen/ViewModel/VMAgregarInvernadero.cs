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
namespace SmartGreen.ViewModel;

public class VMAgregarInvernadero : BaseViewModel
{
    // Comandos
    public ICommand VolverCommand { get; }
    public ICommand ReturnToMenu { get; }
    public ICommand RegistrarInvernadero { get; }

    public VMAgregarInvernadero()
    {
        VolverCommand = new Command(async () => await VolverAtras());
        ReturnToMenu = new Command(async () => await BackToMenu());
        RegistrarInvernadero = new Command(async () => await ResgistrarInvernadero(new InvernaderoModel()));
    }

    // Propiedades privadas
    private string _nombre;
    private string _idInvernadero;
    private string _usuCorreo;
    private string _nombreInvernadero;
    private string _descripcion;
    private int _tipoInvernadero;
    private double _minHumedad;
    private double _maxHumedad;
    private double _minTemperatura;
    private double _maxTemperatura;
    private bool _started;

    private readonly string correo = "alex@gmail.com"; // Se puede cambiar si es necesario

    // Propiedades publicas con notificación de cambios (OnPropertyChanged)
    public string Nombre
    {
        get => _nombre;
        set => SetProperty(ref _nombre, value);
    }

    public string idInvernadero
    {
        get => _idInvernadero;
        set => SetProperty(ref _idInvernadero, value);
    }

    public string UsuCorreo
    {
        get => _usuCorreo;
        set => SetProperty(ref _usuCorreo, value);
    }

    public string NombreInvernadero
    {
        get => _nombreInvernadero;
        set => SetProperty(ref _nombreInvernadero, value);
    }

    public string Descripcion
    {
        get => _descripcion;
        set => SetProperty(ref _descripcion, value);
    }

    public int TipoInvernadero
    {
        get => _tipoInvernadero;
        set => SetProperty(ref _tipoInvernadero, value);
    }

    public double MinHumedad
    {
        get => _minHumedad;
        set => SetProperty(ref _minHumedad, value);
    }

    public double MaxHumedad
    {
        get => _maxHumedad;
        set => SetProperty(ref _maxHumedad, value);
    }

    public double MinTemperatura
    {
        get => _minTemperatura;
        set => SetProperty(ref _minTemperatura, value);
    }

    public double MaxTemperatura
    {
        get => _maxTemperatura;
        set => SetProperty(ref _maxTemperatura, value);
    }

    public bool Start
    {
        get => _started;
        set => SetProperty(ref _started, value);
    }

    // Registrar un invernadero
    private async Task ResgistrarInvernadero(InvernaderoModel inverModel)
    {
        inverModel.idInvernadero = idInvernadero?.Trim();
        inverModel.nombreInvernadero = NombreInvernadero?.Trim();
        inverModel.usuCorreo = correo;
        inverModel.descripcion = Descripcion?.Trim();

        using (var cliente = new HttpClient())
        {
            try
            {
                string json = JsonSerializer.Serialize(inverModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync("https://h387mpbd-5062.usw3.devtunnels.ms/api/Invernadero/RegistrarInvernadero", content);

                if (respuesta.IsSuccessStatusCode)
                {
                    await BackToMenu();
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

    // Volver al menu de invernaderos
    private async Task BackToMenu()
    {
        await Shell.Current.GoToAsync($"/{nameof(MenuInvernaderos)}");
    }

    // Regresar a la página anterior
    private async Task VolverAtras()
    {
        await Shell.Current.GoToAsync("..");
    }
}
