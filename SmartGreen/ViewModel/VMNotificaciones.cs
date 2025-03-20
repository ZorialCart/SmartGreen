using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGreen.ViewModel
{
    public class VMNotificaciones : BaseViewModel
    {
        public ObservableCollection<Notificacion> Notificaciones { get; set; }
        public class Notificacion
        {
            public string Titulo { get; set; }
            public string Cuerpo { get; set; }
            public DateTime FechaHora { get; set; }
        }

        public VMNotificaciones()
        {
            Notificaciones = new ObservableCollection<Notificacion>
            {
                new Notificacion
                {
                    Titulo = "Invernadero Squish",
                    Cuerpo = "El sistema de riego dejó de funcionar.",
                    FechaHora = DateTime.Now
                },
                new Notificacion
                {
                    Titulo = "Invernadero Squish",
                    Cuerpo = "Temperatura debajo de 20°.",
                    FechaHora = DateTime.Now.AddHours(-1)
                },
                new Notificacion
                {
                    Titulo = "Invernadero de Corick",
                    Cuerpo = "La humedad está muy alta! 70%",
                    FechaHora = DateTime.Now.AddDays(-1)
                }
            };

        }
    }
}
