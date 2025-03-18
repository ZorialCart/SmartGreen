using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SmartGreen.Clases
{
    public class AuthService
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

        // Método asincrónico para obtener el token
        public static async Task<string?> GetTokenAsync()
        {
           var token = await SecureStorage.GetAsync("token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }
            return null;
        }

        // Método asincrónico para verificar si el usuario está autenticado
        public static async Task<bool> IsAuthenticatedAsync()
        {
            string token = await GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                return true;
            }
            return false;

        }

        // Método asincrónico para cerrar sesión y eliminar el token
        public static async Task LogOutAsync()
        {
           await SecureStorage.SetAsync("token", string.Empty);
        }
    }
}
