using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakerDataClient.Models;

namespace TakerDataClient.Interface.IProviders
{
    public interface IFormsProvider
    {

        Task<JsonResponseGetForm> GetUserForm(JsonRequestGetForm model);
        Task<JsonResponseSaveForm> SaveUserResponses(JsonRequestSaveForm model);
    }
}
