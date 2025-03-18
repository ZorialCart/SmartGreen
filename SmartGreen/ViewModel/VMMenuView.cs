﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartGreen.Model;

namespace SmartGreen.ViewModel
{
    internal class VMMenuView : BaseViewModel
    {
        public ICommand OpenFlyoutCommand { get; }
        public VMMenuView()
        {
            OpenFlyoutCommand = new Command(OpenFlyout);
        }
        private void OpenFlyout()
        {
            // Abre el menú del Flyout
            Shell.Current.FlyoutIsPresented = true;
        }


        public ObservableCollection<ViveroModel> Invernaderos { get; set; }

        public VMMenuView(INavigation navigation)
        {
            Navigation = navigation;
            // Simulación de datos; aquí conectarías con tu base de datos
            Invernaderos = new ObservableCollection<ViveroModel>
    {
    new ViveroModel { Nombre = "Invernadero Squish" },
    new ViveroModel { Nombre = "Invernadero Orquidea"},

    };
        }
    }

}

