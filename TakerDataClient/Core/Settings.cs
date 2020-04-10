using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTakerData.Interface.ICore;

namespace WebTakerData.Core
{
    public class Settings : ISettings
    {
        public string ApplicationName { get; set; }
        public string SessionTimeout { get; set; }
        public string UrlLogOut { get; set; }
        public string UrlHome { get; set; }
        public string JsonWebTokenSecretKey { get; set; }
        public string AesWebTokenSecretKey { get; set; }
        public string RsaCryptoSecretKey { get; set; }
    }
}
