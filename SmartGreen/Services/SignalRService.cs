using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SmartGreen.Model;
using System.Threading.Tasks;

namespace SmartGreen.Services
{
    public class SignalRService
    {
        private readonly HubConnection _hubconnection;
        private InverStatusModel _lastInverStatus;

        public event Action<InverStatusModel> OnStatusReceived;

        public SignalRService()
        {
            _hubconnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5062/inverStatusHub")
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task ConnectAsync(string IdInvernadero)
        {
            _hubconnection.On<InverStatusModel>("ReceiveStatus", (status) => 
            {
                if(status.idInvernadero == IdInvernadero)
                {
                    _lastInverStatus = status;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        OnStatusReceived?.Invoke(_lastInverStatus);
                    });
                }
            });

            await _hubconnection.StartAsync();
        }

        public InverStatusModel GetLastStatus()
        {
            return _lastInverStatus;
        }
    }
}
