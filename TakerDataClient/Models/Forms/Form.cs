using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakerDataClient.Models.Forms
{
    public class Form
    {

        public string idform { get; set; }
        public string titulo { get; set; }
        public string fecha_vigencia { get; set; }

        public string comentario { get; set; }

        public string estado { get; set; }

        public string idusuario { get; set; }

        public string idempresa { get; set; }

        public List<Control> controls { get; set; }
    }
}
