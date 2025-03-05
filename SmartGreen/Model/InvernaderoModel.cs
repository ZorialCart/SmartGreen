using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGreen.Model
{
    public class InvernaderoModel
    {
        public string? id { get; set; }
        public string? idInvernadero { get; set; }
        public string? usuCorreo { get; set; }
        public string? nombreInvernadero { get; set; }
        public string? descripcion { get; set; }
        public int tipoInvernadero { get; set; }
        public double minHumedad { get; set; }
        public double maxHumedad { get; set; }
        public double minTemperatura { get; set; }
        public double maxTemperatura { get; set; }
        public bool started { get; set; }
    }
}
