using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kataBanco.api.Models
{
    public class CuentaResponse
    {
        public int numero_cuenta { get; set; }
        public decimal saldo { get; set; }
        public string fecha_apertura { get; set; }
        public string estado { get; set; }
    }
}