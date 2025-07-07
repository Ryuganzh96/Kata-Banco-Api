using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using kataBancoApi.Models;
using System.Threading.Tasks;
using kataBancoApi.Services;
using kataBanco.api.Services;
using kataBanco.api.Responses;
using kataBanco.api.Helpers;

namespace kataBancoApi.Controllers
{

    [RoutePrefix("api/cliente")]
    public class ClientController : ApiController
    {
        [HttpPost]
        [Route("consulta")]
        public async Task<IHttpActionResult> ConsultarInformacion(ClientRequest request)
        {
            try
            {
                var cuentas = await CuentaService.ConsultarCuentas(request);
                var historial = await HistorialService.ConsultarHistorial(request);
                var datacredito = await DatacreditoService.Consultar(request);

                var totalDiasMora = historial.vigentes.Sum(c => c.dias_mora);
                var totalSaldoMora = historial.vigentes.Sum(c => c.mora);
                var enMora = historial.vigentes.Any(c => c.mora > 0);

                var todosCreditos = historial.vigentes.Concat(historial.cancelados)
                    .OrderByDescending(c => DateTime.Parse(c.fecha_apertura))
                    .ToList();

                var ultimo = todosCreditos.FirstOrDefault();

                // Traducir estados en historial
                historial.vigentes.ForEach(c => c.estado = MaestrosHelpers.TraducirEstado(c.estado));
                historial.cancelados.ForEach(c => c.estado = MaestrosHelpers.TraducirEstado(c.estado));

                // Traducir estados en cuentas
                cuentas.ForEach(c => c.estado = MaestrosHelpers.TraducirEstado(c.estado));

                var resultado = new
                {
                    Documento = new
                    {
                        Tipo = MaestrosHelpers.TraducirTipoDocumento(request.TipoDocumento),
                        Numero = request.NumeroDocumento,
                        Pais = MaestrosHelpers.TraducirPais(request.Pais)
                    },
                    EnMora = enMora ? "verdadero" : "falso",
                    DiasMoraTotales = totalDiasMora,
                    SaldoMoraTotal = totalSaldoMora,
                    TotalCuentas = cuentas.Count,
                    TotalCreditosVigentes = historial.vigentes.Count,
                    TotalCreditosCancelados = historial.cancelados.Count,
                    TotalDatacredito = new
                    {
                        Creditos = datacredito.creditos?.Count ?? 0,
                        Hipotecarios = datacredito.hipotecarios?.Count ?? 0,
                        Ahorros = datacredito.ahorros?.Count ?? 0,
                        Tarjetas = datacredito.tarjetas?.Count ?? 0,
                        Huellas = datacredito.huellas?.Count ?? 0
                    },
                    UltimoCredito = ultimo != null ? new
                    {
                        Numero = ultimo.numero_cuenta,
                        Estado = MaestrosHelpers.TraducirEstado(ultimo.estado),
                        Saldo = ultimo.saldo
                    } : null,
                    Detalle = new
                    {
                        Cuentas = cuentas,
                        Historial = historial,
                        Datacredito = datacredito
                    }
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;

                string servicio = "desconocido";
                if (mensaje.Contains("cuentas")) servicio = "CuentaService";
                else if (mensaje.Contains("historial")) servicio = "HistorialService";
                else if (mensaje.Contains("Datacrédito")) servicio = "DatacreditoService";
                else if (mensaje.Contains("token")) servicio = "LoginService";

                return Content(System.Net.HttpStatusCode.InternalServerError, new
                {
                    error = $"Ocurrió un error en el servicio: {servicio}",
                    detalle = mensaje
                });
            }
        }


    }
}