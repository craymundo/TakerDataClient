using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTakerData.Utilities
{
    public static class JwtToken
    {
        public static string GetEncrytToken(object obj, string key)
        {

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();

            IJsonSerializer serializer = new JsonNetSerializer();

            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(obj, key);

            return token;
        }

        public static T GetDecryptToken<T>(string jwtToken, string key)
        {
            T model = Activator.CreateInstance<T>();

            IJsonSerializer serializer = new JsonNetSerializer();

            IDateTimeProvider dateTimeProvider = new UtcDateTimeProvider();

            IJwtValidator validate = new JwtValidator(serializer, dateTimeProvider);

            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            IJwtDecoder decode = new JwtDecoder(serializer, validate, urlEncoder);

            model = decode.DecodeToObject<T>(jwtToken, key, true);

            return model;
        }
    }
}
