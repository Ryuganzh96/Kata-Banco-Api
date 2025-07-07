using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kataBanco.api.Models
{
    public class HistorialCredito
    {
        public int numero_cuenta { get; set; }
        public decimal saldo { get; set; }
        public decimal mora { get; set; }
        public int dias_mora { get; set; }
        public decimal capital { get; set; }
        public string fecha_apertura { get; set; }
        public string fecha_cancelacion { get; set; }
        public string estado { get; set; }

    }

    public class HistorialResponse
    {
        public List<HistorialCredito> cancelados { get; set; }
        public List<HistorialCredito> vigentes { get; set; }
    }
}