using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Pregunta1
{
   public class Asistente
    {
        public static int contador = 0;
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public double Importe { get;set; }
        public int Cantidad { get; set; }

        public string generaCodigo()
        {
            contador++;
            return "AEV-" + contador.ToString("000");
        }

        public decimal Costo()
        {
            return Precio()* Cantidad;
        }

        public virtual decimal Precio()
        {
            decimal PrecioImporte = 250;
            switch (Categoria)
            {
                case "Normal":
                    PrecioImporte = 250;
                    break;

                case "VIP":
                    PrecioImporte = 400;
                    break;

                case "Super VIP":
                    PrecioImporte = 500;
                    break;

            }
            return PrecioImporte;
        }
    }
}
