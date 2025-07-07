using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kataBanco.api.Responses
{
    public class ClienteResponse
    {
        public bool EnMora { get; set; }
        public int DiasMoraTotales { get; set; }
        public decimal SaldoMoraTotal { get; set; }

        public UltimoCreditoResponse UltimoCredito { get; set; }

        public object Cuentas { get; set; }
        public object Historial { get; set; }
        public object Datacredito { get; set; }
    }

    public class UltimoCreditoResponse
    {
        public int Numero { get; set; }
        public string Estado { get; set; }
        public decimal Saldo { get; set; }
    }
}