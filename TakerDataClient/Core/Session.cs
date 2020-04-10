using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTakerData.Core
{
    public static class Session
    {
        public static async Task<T> GetData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(data));
        }

        public static async Task SetData(this ISession session, string key, object value)
        {
            session.SetString(key, await Task.Factory.StartNew(() => JsonConvert.SerializeObject(value)));
        }
    }
}
