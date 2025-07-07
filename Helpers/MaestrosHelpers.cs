using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kataBanco.api.Helpers
{
    public static class MaestrosHelpers
    {
        private static readonly Dictionary<string, string> Estados = new Dictionary<string, string>
        {
            { "1", "Abierto/Activo" },
            { "2", "Cancelado" },
            { "3", "Congelado" },
            { "4", "Refinanciado" },
            { "5", "Castigado" },
            { "6", "En cobro jurídico" },
            { "7", "Bloqueado" }
        };

        private static readonly Dictionary<string, string> Paises = new Dictionary<string, string>
        {
            { "1", "Colombia" },
            { "2", "Ecuador" },
            { "3", "Perú" },
            { "4", "Venezuela" },
            { "5", "Panamá" },
            { "6", "Brasil" },
            { "7", "Argentina" },
            { "8", "Estados Unidos" },
            { "9", "España" }
        };

        private static readonly Dictionary<string, string> TiposDocumento = new Dictionary<string, string>
        {
            { "1", "Cédula de ciudadanía" },
            { "2", "NIT" },
            { "3", "Cédula de Extranjería" },
            { "4", "Pasaporte" }
        };

        public static string TraducirEstado(string codigo) =>
            Estados.TryGetValue(codigo, out var val) ? val : "Desconocido";

        public static string TraducirPais(string codigo) =>
            Paises.TryGetValue(codigo, out var val) ? val : "Desconocido";

        public static string TraducirTipoDocumento(string codigo) =>
            TiposDocumento.TryGetValue(codigo, out var val) ? val : "Desconocido";
    }

}