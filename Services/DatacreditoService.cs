using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using kataBanco.api.Helpers;
using kataBanco.api.Models;
using kataBancoApi.Models;
using kataBancoApi.Services;
using Newtonsoft.Json;

namespace kataBanco.api.Services
{
    public class DatacreditoService
    {
        private static readonly string url = "https://9eb36e15-ab57-4b35-bf50-27c4780f55f8.mock.pstmn.io/clientes/datacredito";

        public static async Task<DatacreditoResponse> Consultar(ClientRequest request)
        {
            try
            {
                var token = await LoginService.ObtenerToken();

                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var body = new
                    {
                        tipoDocumento = request.TipoDocumento,
                        numeroDoc = request.NumeroDocumento,
                        país = request.Pais
                    };

                    var contenido = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                    var respuesta = await cliente.PostAsync(url, contenido);

                    if (!respuesta.IsSuccessStatusCode)
                        throw new Exception("Error al consultar Datacrédito");

                    var json = await respuesta.Content.ReadAsStringAsync();

                    LoggerHelper.LogConsumo("DatacreditoService", $"Consulta exitosa para documento {request.NumeroDocumento}");

                    return JsonConvert.DeserializeObject<DatacreditoResponse>(json);
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError($"DatacreditoService: {ex.Message}");
                throw;
            }
        }
    }

}