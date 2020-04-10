using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTakerData.Interface.ICore
{
    public interface ISettings
    {
        string ApplicationName { get; set; }
        string SessionTimeout { get; set; }
        string UrlLogOut { get; set; }
        string UrlHome { get; set; }
        string JsonWebTokenSecretKey { get; set; }
        string AesWebTokenSecretKey { get; set; }
        string RsaCryptoSecretKey { get; set; }
    }
}
