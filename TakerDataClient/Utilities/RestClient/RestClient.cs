using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebTakerData.Interface.ICore;
using WebTakerData.Utilities;

namespace WebTottusIntranet.Utils.RestClient
{
    public class RestClient: IRestClient
    {
        private readonly IServices _services;

        private const string _mediaType = "application/json";

        public RestClient( IServices services)
        {
            _services = services;
           
        }

        public async Task<IEnumerable<T>> GetAsync<T>(string path) where T : class, new()
        {
            try
            {
                var responseBody = string.Empty;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_services.Api);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));

                    var response = await client.GetAsync(_services.Api + path);

                    response.EnsureSuccessStatusCode();

                    responseBody = await response.Content.ReadAsStringAsync();
                }

                var list = JsonConvert.DeserializeObject<IEnumerable<T>>(responseBody);

                return list;
            }
            catch (WebException ex)
            {
                throw new WebException("RestClient.GetAsync error al invocar al servicio. " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("RestClient.GetAsync error " + ex.Message);
            }
        }

        public async Task<T> PostAsync<T>(string path, object obj)
        {
            var responseBody = await PostAsync(path, obj);

            var item = JsonConvert.DeserializeObject<T>(responseBody);

            return item;
        }

        public async Task<IEnumerable<T>> PostListAsync<T>(string path, object obj) where T : class, new()
        {
            var responseBody = await PostAsync(path, obj);

            var list = JsonConvert.DeserializeObject<IEnumerable<T>>(responseBody);

            return list;
        }

        private async Task<string> PostAsync(string path, object obj)
        {
            var responseBody = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_services.Api);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));

                    var postBody = new StringContent(JsonConvert.SerializeObject(obj).ToString(),
                        Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(_services.Api + path, postBody);

                    response.EnsureSuccessStatusCode();

                    responseBody = await response.Content.ReadAsStringAsync();
                }
            }
            catch (WebException ex)
            {
                throw new WebException("RestClient.PostListAsync error al invocar al servicio. " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("RestClient.PostListAsync error " + ex.Message);
            }

            return responseBody;
        }

        public async Task<T> PostFileAsync<T>(string path, IFormFile file,  object obj)
        {
            var responseBody = await PostFileAsync(path, file,  obj);

            var item = JsonConvert.DeserializeObject<T>(responseBody);

            return item;
        }

        private async Task<string> PostFileAsync(string path, IFormFile file ,  object obj)
        {
            var responseBody = string.Empty;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_services.Api);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));

                   

                    using (var content = new MultipartFormDataContent())
                    {

                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        content.Add(new StreamContent(file.OpenReadStream())
                        {
                            Headers =
                    {
                        ContentLength = file.Length,
                        ContentType = new MediaTypeHeaderValue(file.ContentType)
                    }
                        },  "files", fileName);


                        foreach (PropertyInfo theProperties in obj.GetType().GetProperties())
                        {
                            var objValue = theProperties.GetValue(obj);
                            if (objValue == null)
                                objValue = "";
                            content.Add(new StringContent(objValue.ToString()), theProperties.Name);
                        }



                        var response = await client.PostAsync(_services.Api + path, content);

                        response.EnsureSuccessStatusCode();

                        responseBody = await response.Content.ReadAsStringAsync();
                    }




                }
            }
            catch (WebException ex)
            {
                throw new WebException("RestClient.PostExtAsync error al invocar al servicio. " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("RestClient.PostExtAsync error " + ex.Message);
            }

            return responseBody;
        }


    }
}
