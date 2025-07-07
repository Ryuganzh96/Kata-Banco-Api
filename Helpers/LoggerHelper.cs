using System;
using System.IO;
using System.Web;

namespace kataBanco.api.Helpers
{
    public static class LoggerHelper
    {
        private static readonly string logPath = HttpContext.Current.Server.MapPath("~/logs");

        public static void LogError(string mensaje)
        {
            try
            {
                Directory.CreateDirectory(logPath);
                string ruta = Path.Combine(logPath, "errores.log");
                File.AppendAllText(ruta, $"[{DateTime.Now}] ERROR: {mensaje}{Environment.NewLine}");
            }
            catch { }
        }

        public static void LogConsumo(string servicio, string resultado)
        {
            try
            {
                Directory.CreateDirectory(logPath);
                string ruta = Path.Combine(logPath, "consumos.log");
                File.AppendAllText(ruta, $"[{DateTime.Now}] {servicio}: {resultado}{Environment.NewLine}");
            }
            catch { }
        }
    }
}