using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGreen.Model;

public class InvernaderoModel
{

        public string? id { get; set; }    
    public string? idInvernadero { get; set; }
    public string? UsuCorreo { get; set; }
    public string? nombreInvernadero { get; set; }
    public int TipoInvernadero { get; set; }
    public double MinHumedad { get; set; }
    public double MaxHumedad { get; set; }
    public double MinTemperatura { get; set; }
    public double MaxTemperatura { get; set; }
    public bool Started { get; set; }

}