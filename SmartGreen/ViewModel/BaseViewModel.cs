﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SmartGreen.ViewModel
{
        public class BaseViewModel : INotifyPropertyChanged
        {
        public INavigation Navigation;

        public event PropertyChangedEventHandler PropertyChanged;
            public void OnpropertyChanged([CallerMemberName] string nombre = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
            }
            private ImageSource foto;
            public ImageSource Foto
            {
                get { return foto; }
                set
                {
                    foto = value;
                    OnpropertyChanged();
                }
            }

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public async Task DisplayAlert(string title, string message, string cancel)
            {
                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
            }

            public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
            {
                return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
            }

            protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<T>.Default.Equals(field, value))
                {
                    return false;
                }
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            private string _title;
            public string Title
            {
                get { return _title; }
                set
                {
                    SetProperty(ref _title, value);
                }
            }

            private bool _isBusy;
            public bool IsBusy
            {
                get { return _isBusy; }
                set
                {
                    SetProperty(ref _isBusy, value);
                }
            }
            private bool _IsRefreshview;
            public bool IsRefreshview
            {
                get { return _IsRefreshview; }
                set
                {
                    SetProperty(ref _IsRefreshview, value);
                }
            }
            private bool _isVisble;
            public bool IsVisble
            {
                get { return _isVisble; }
                set
                {
                    SetProperty(ref _isVisble, value);
                }
            }
            protected void SetValue<T>(ref T backingFieled, T value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<T>.Default.Equals(backingFieled, value))
                {
                    return;
                }
                backingFieled = value;
                OnPropertyChanged(propertyName);
            }
        }
    }