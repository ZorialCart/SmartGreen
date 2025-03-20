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
        public int minHumedad { get; set; }
        public int maxHumedad { get; set; }
        public int minTemperatura { get; set; }
        public int maxTemperatura { get; set; }
        public bool started { get; set; }
    }
}
