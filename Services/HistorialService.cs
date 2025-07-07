using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using kataBancoApi.Models;
using kataBanco.api.Models;
using Newtonsoft.Json;
using kataBancoApi.Services;
using kataBanco.api.Helpers;

namespace kataBanco.api.Services
{
    public class HistorialService
    {
        private static readonly string url = "https://9eb36e15-ab57-4b35-bf50-27c4780f55f8.mock.pstmn.io/clientes/historial-crediticio";

        public static async Task<HistorialResponse> ConsultarHistorial(ClientRequest request)
        {
            try
            {
                var token = await LoginService.ObtenerToken();

                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var query = $"?tipoDoc={request.TipoDocumento}&numeroDocumento={request.NumeroDocumento}&país={request.Pais}";
                    var fullUrl = url + query;

                    var respuesta = await cliente.GetAsync(fullUrl);

                    if (!respuesta.IsSuccessStatusCode)
                        throw new Exception("Error al consultar historial crediticio");

                    var json = await respuesta.Content.ReadAsStringAsync();

                    LoggerHelper.LogConsumo("HistorialService", $"Consulta exitosa para documento {request.NumeroDocumento}");

                    return JsonConvert.DeserializeObject<HistorialResponse>(json);
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"HistorialService: {ex.Message}");
                throw;
            }
        }
    }

}