﻿using SmartGreen.View;
using SmartGreen.View.RecoveryPass;
using SmartGreen.View.ViveroView;
namespace SmartGreen
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Login", typeof(Login));
            Routing.RegisterRoute("MenuView", typeof(MenuView));
            Routing.RegisterRoute("Register", typeof(Register));
            Routing.RegisterRoute("Recovery1", typeof (Recovery1));
            Routing.RegisterRoute("Recovery2", typeof(Recovery2));
            Routing.RegisterRoute("MenuInvernaderos", typeof(MenuInvernaderos));
            Routing.RegisterRoute("AgregarInvernadero", typeof(AgregarInvernadero));


        }
    }
}
