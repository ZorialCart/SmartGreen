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
	public VMAgregarInvernadero()
	{
		
	}

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

    string correo = "alex@gmail.com";
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

    public string idInvernadero
    {
        get => _idInvernadero;
        set
        {
            if (_idInvernadero != value)
            {
                _idInvernadero = value;
                OnpropertyChanged(nameof(idInvernadero));
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
                OnpropertyChanged(nameof(UsuCorreo));
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
                OnpropertyChanged(nameof(nombreInvernadero));
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
                OnpropertyChanged(nameof(TipoInvernadero));
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
                OnpropertyChanged(nameof(MinHumedad));
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
                OnpropertyChanged(nameof(MaxHumedad));
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
                OnpropertyChanged(nameof(MinTemperatura));
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
                OnpropertyChanged(nameof(MaxTemperatura));
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
                OnpropertyChanged(nameof(Start));
            }
        }
    }



    internal async Task ResgistrarInvernadero(InvernaderoModel inverModel)
    {
        inverModel.idInvernadero = idInvernadero.Trim();
        inverModel.nombreInvernadero = nombreInvernadero.Trim();
        inverModel.UsuCorreo = correo;


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


    public async Task BackToMenu()
    {
        await Shell.Current.GoToAsync($"/{nameof(MenuInvernaderos)}");
    }


    //Comandos
    public ICommand ReturnToMenu => new Command(async () => await BackToMenu());
    public ICommand RegistrarInvernadero => new Command(async () => await ResgistrarInvernadero(new InvernaderoModel()));

}