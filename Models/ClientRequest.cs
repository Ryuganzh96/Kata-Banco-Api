using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kataBancoApi.Models
{
    public class ClientRequest
    {
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Pais { get; set; }

    }
}