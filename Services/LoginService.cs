using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using kataBanco.api.Helpers;

namespace kataBancoApi.Services
{
    public class LoginService
    {
        private static string _token = null;
        private static DateTime _vencimiento;

        public static async Task<string> ObtenerToken()
        {
            if (_token != null && DateTime.UtcNow < _vencimiento)
                return _token;

            try
            {
                var url = "https://9eb36e15-ab57-4b35-bf50-27c4780f55f8.mock.pstmn.io/login";
                var cliente = new HttpClient();

                var body = new
                {
                    usuario = "kata",
                    clave = "k474.2025#"
                };

                var contenido = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                var respuesta = await cliente.PostAsync(url, contenido);

                if (!respuesta.IsSuccessStatusCode)
                    throw new Exception("Error al obtener token");

                var json = await respuesta.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(json);

                _token = data.token;
                _vencimiento = DateTime.Parse(data.fecha_vencimiento.ToString());

                LoggerHelper.LogConsumo("LoginService", "Token obtenido exitosamente");

                return _token;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"LoginService: {ex.Message}");
                throw;
            }
        }
    }
}