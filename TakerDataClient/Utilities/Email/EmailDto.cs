using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTakerData.Utils.Email
{
    public class EmailDto
    {
        public string DisplayNameEnvio { get; set; }
        public string EmailEnvio { get; set; }
        public string EmailDestino { get; set; }
        public string EmailDestinoCC { get; set; }
        public string Asunto { get; set; }
        public string Body { get; set; }
        public bool EsBodyHtml { get; set; }
        public string PassWord { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string NomImgFirma { get; set; }
    }
}
