using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakerDataClient.Interface.IProviders;
using TakerDataClient.Models;
using WebTakerData.Core;
using WebTakerData.Interface.ICore;
using WebTottusIntranet.Utils.RestClient;

namespace TakerDataClient.Providers
{
    public class FormsProvider : IFormsProvider
    {
        private readonly IRestClient requestclient;

        public FormsProvider(IRestClient _requestclient)
        {
            this.requestclient = _requestclient;
        }

        public async Task<JsonResponseGetForm> GetUserForm(JsonRequestGetForm model)
        {
            return await requestclient.PostAsync<JsonResponseGetForm>(Constante.Api.GetUserForm, model);
        }


        public async Task<JsonResponseSaveForm> SaveUserResponses(JsonRequestSaveForm model)
        {
            return await requestclient.PostAsync<JsonResponseSaveForm>(Constante.Api.SaveUserResponses, model);
        }
    }
}
