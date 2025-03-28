using CommunityToolkit;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using SmartGreen.ViewModel;
namespace SmartGreen;

public partial class PopUpHumedad : ContentView
{
    private double _startY;

    public PopUpHumedad()
    {
        InitializeComponent();
        IsVisible = false; // Oculto por defecto
    }

    public async Task Show()
    {
        IsVisible = true;
        await SheetContent.TranslateTo(0, 0, 300, Easing.SinOut); // Animación de subida
    }

    public async void CloseSheet(object sender, EventArgs e)
    {
        await SheetContent.TranslateTo(0, 400, 300, Easing.SinIn); // Deslizar hacia abajo
        IsVisible = false;
    }

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (e.StatusType == GestureStatus.Started)
        {
            _startY = e.TotalY;
        }
        else if (e.StatusType == GestureStatus.Running)
        {
            SheetContent.TranslationY = Math.Max(0, e.TotalY);
        }
        else if (e.StatusType == GestureStatus.Completed)
        {
            if (e.TotalY > 100) // Si se desliza bastante hacia abajo, se cierra
            {
                CloseSheet(null, null);
            }
            else
            {
                SheetContent.TranslateTo(0, 0, 300, Easing.SinOut); // Vuelve a su posición
            }
        }
    }

    //private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    //{
    //    HumidityLabel.Text = $"Humedad {e.NewValue:F0}%";
    //}
}