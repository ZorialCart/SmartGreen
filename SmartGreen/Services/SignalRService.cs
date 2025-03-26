using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Dispatching;
using SmartGreen.Model;

namespace SmartGreen.Services
{
    public class SignalRService
    {
        private readonly HubConnection _hubconnection;
        private bool _isConnected;
        private InverStatusModel _lastInverStatus;

        public event Action<InverStatusModel> OnStatusReceived;

        public SignalRService()
        {
            _hubconnection = new HubConnectionBuilder()
                .WithUrl("http://192.168.1.11:5062/inverStatusHub") // Cambia la URL según sea necesario
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task ConnectAsync(string IdInvernadero)
        {
            if (_isConnected)
                return; // Evita múltiples conexiones

            _hubconnection.On<InverStatusModel>("ReceiveStatus", (status) =>
            {
                if (status.idInvernadero == IdInvernadero)
                {
                    _lastInverStatus = status;

                    // Asegurar la ejecución en el hilo principal de la UI
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        OnStatusReceived?.Invoke(_lastInverStatus);
                    });
                }
            });

            try
            {
                await _hubconnection.StartAsync();

                if (_hubconnection.State == HubConnectionState.Connected)
                {
                    _isConnected = true;
                    Console.WriteLine("Conectado a SignalR correctamente.");

                    await _hubconnection.InvokeAsync("SubscribeToInvernadero", IdInvernadero);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a SignalR: {ex.Message}");
            }
        }

        public async Task DisconnectAsync()
        {
            if (!_isConnected) return;

            try
            {
                await _hubconnection.StopAsync();
                _isConnected = false;
                Console.WriteLine("Desconectado de SignalR.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desconectar SignalR: {ex.Message}");
            }
        }

        public InverStatusModel GetLastStatus()
        {
            return _lastInverStatus;
        }
    }
}

