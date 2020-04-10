using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakerDataClient.Models.Forms;

namespace TakerDataClient.Models
{
    public class JsonRequestSaveForm
    {
        public string idUsuario { get; set; }
        public string idEmpresa { get; set; }
        public string idForm { get; set; }

        public string token { get; set; }
        public List<ControlValue> controls { get; set; }
    }
}
