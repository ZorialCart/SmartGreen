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
        public string? nombreInvernadero { get; set; }
        public double currentHumedad { get; set; }
        public double currentTemperatura { get; set; }
        public double currentLuz { get; set; }
        public DateTime cecha { get; set; } = DateTime.UtcNow;
    }
}
