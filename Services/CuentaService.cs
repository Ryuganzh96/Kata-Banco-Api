using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using kataBancoApi.Models;
using Newtonsoft.Json;
using kataBanco.api.Models;
using kataBancoApi.Services;
using kataBanco.api.Helpers;

namespace kataBanco.api.Services
{
    public class CuentaService
    {
        private static readonly string url = "https://9eb36e15-ab57-4b35-bf50-27c4780f55f8.mock.pstmn.io/clientes/cuentas";

        public static async Task<List<CuentaResponse>> ConsultarCuentas(ClientRequest request)
        {
            try
            {
                var token = await LoginService.ObtenerToken();

                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var contenido = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                    var respuesta = await cliente.PostAsync(url, contenido);

                    if (!respuesta.IsSuccessStatusCode)
                        throw new Exception("Error al consultar cuentas");

                    var json = await respuesta.Content.ReadAsStringAsync();

                    LoggerHelper.LogConsumo("CuentaService", $"Consulta exitosa para documento {request.NumeroDocumento}");

                    return JsonConvert.DeserializeObject<List<CuentaResponse>>(json);
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"CuentaService: {ex.Message}");
                throw;
            }
        }
    }
}