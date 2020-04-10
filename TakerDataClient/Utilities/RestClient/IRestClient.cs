using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTottusIntranet.Utils.RestClient
{
    public interface IRestClient
    {
        Task<IEnumerable<T>> GetAsync<T>(string path) where T : class, new();

        Task<T> PostAsync<T>(string path, object obj);

        Task<IEnumerable<T>> PostListAsync<T>(string path, object obj) where T : class, new();

        Task<T> PostFileAsync<T>(string path, IFormFile file,  object obj);

    }
}
