using SmartGreen.Clases;
using SmartGreen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartGreen.ViewModel
{
    class VMGreenHouseView : BaseViewModel
    {
        private bool _started;
        private string _estadoFlujo;
        private double _humedad;
        private double _temperatura;
        public string HumedadPorcentaje { get; set; } = "45%"; // Ejemplo del porcentaje
        public IDrawable CircularProgressDrawable { get; set; } = new CircularProgressDrawable(1f);

        public VMGreenHouseView(InverStatusModel inverStatus)
        {
            Humedad = inverStatus.CurrentHumedad;
            Temperatura = inverStatus.CurrentTemperatura;
            Started = true;
        }

        public string EstadoFlujo
        {
            get { return _estadoFlujo; }
            set { SetValue(ref _estadoFlujo, value); }
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
