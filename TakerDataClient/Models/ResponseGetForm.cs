using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakerDataClient.Models.Forms;

namespace TakerDataClient.Models
{
    public class ResponseGetForm
    {
        public string nombre { get; set; }
        public string email { get; set; }
        public string dni { get; set; }
        public string celular { get; set; }
        
        public Form form { get; set; }
    }
}


