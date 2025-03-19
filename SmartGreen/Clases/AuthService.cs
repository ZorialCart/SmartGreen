using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;

namespace SmartGreen.Clases
{
    public static class AuthService
    {
        public static async Task SaveTokenAsync(string token)
        {
            try
            {
                await SecureStorage.SetAsync("token", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el token: " + ex.Message);
            }
        }

     
        public static async Task<string?> GetTokenAsync()
        {
            var token = await SecureStorage.GetAsync("token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }
            else
            {
                Console.WriteLine("No se encuentra el token.");
                return null;
            }
        }

        public static async Task<bool> IsAuthenticatedAsync()
        {
            string token = await GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                return true;
            }
            return false;

        }

        public static async Task LogOutAsync()
        {
           await SecureStorage.SetAsync("token", string.Empty);
        }


        public static async Task SaveEmail(string correo)
        {
            try
            {
                await SecureStorage.SetAsync("correo", correo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el correo: " + ex.Message);
            }
        }

        public static async Task<string?> GetUserEmailAsync()
    {
        var correo = await SecureStorage.GetAsync("correo");
        if (!string.IsNullOrWhiteSpace(correo))
        {
            return correo;
        }
        return null;
    }
    }
}
