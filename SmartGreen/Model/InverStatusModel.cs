using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGreen.Model
{
    public class InverStatusModel
    {
        public string? id { get; set; }
        public string? idInvernadero { get; set; }
        public double CurrentHumedad { get; set; }
        public double CurrentTemperatura { get; set; }
        public double CurrentLuz { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
