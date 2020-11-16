using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pregunta2
{
    public class Serie
    {
        public static int contador = 0;
        public string Codigo {get ;set;}
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Estreno { get; set; }
        public int Duracion { get; set; }
        public double ImporteProd { get; set; }
        
        public string generaCodigo()
        {
            contador++;
            return "SE-" + contador.ToString("0000");
        }

    }
}
