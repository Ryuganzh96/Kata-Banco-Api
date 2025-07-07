using System;
using System.Collections.Generic;

namespace kataBanco.api.Models
{
    public class ProductoDatacredito
    {
        public int numero_cuenta { get; set; }
        public string entidad { get; set; }
        public string estado { get; set; }
        public string comportamiento { get; set; }
        public string franquicia { get; set; }
        public int cupo { get; set; }
        public string fecha_apertura { get; set; }
    }

    public class HuellaConsulta
    {
        public string entidad { get; set; }
        public string fecha { get; set; }
    }

    public class DatacreditoResponse
    {
        public List<ProductoDatacredito> creditos { get; set; }
        public List<ProductoDatacredito> hipotecarios { get; set; }
        public List<ProductoDatacredito> ahorros { get; set; }
        public List<ProductoDatacredito> tarjetas { get; set; }
        public List<HuellaConsulta> huellas { get; set; }
    }
}