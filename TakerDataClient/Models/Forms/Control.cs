using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakerDataClient.Models.Forms
{
    public class Control
    {
        public string id { get; set; }
        public string tipo { get; set; }
        public string pregunta { get; set; }
        public List<Opciones> opciones { get; set; }
        public string tipo_respuesta { get; set; }
        public string respuesta_obligatoria { get; set; }
        public string respuesta_larga { get; set; }
        public string tipo_simbolo { get; set; }
        public string niveles { get; set; }
        public string restriccion { get; set; }
        public string valor1 { get; set; }
        public string valor2 { get; set; }
        public string controlId { get; set; }
    }

}
