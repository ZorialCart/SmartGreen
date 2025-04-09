using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGreen.Model
{
    public class ChangeInverStatusModel
    {
        public string? idInvernadero { get; set; }
        public int MinHumedad { get; set; }
        public int MaxHumedad { get; set; }
        public int MinTemperatura { get; set; }
        public int MaxTemperatura { get; set; }
    }
}
