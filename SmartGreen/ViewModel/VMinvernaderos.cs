using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SmartGreen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartGreen.ViewModel;

internal class VMinvernaderos : BaseViewModel
{
    public ObservableCollection<ModelViveros> Invernaderos { get; set; }

    public VMinvernaderos(INavigation navigation)
    {
        Navigation = navigation;
        // Simulación de datos; aquí conectarías con tu base de datos
        Invernaderos = new ObservableCollection<ModelViveros>
    {
    new ModelViveros { Nombre = "Invernadero Squish" },
    new ModelViveros { Nombre = "Invernadero Orquidea"},

    };
    }
}