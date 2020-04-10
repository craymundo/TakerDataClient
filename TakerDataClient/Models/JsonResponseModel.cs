using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakerDataClient.Models
{
    public class JsonResponseModel
    {
        public bool success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public object result { get; set; }

        public JsonResponseModel(bool success, object result, string message = null)
        {
            this.success = success;
            this.result = result;
            this.message = message;
            this.code = string.Empty;
        }
    }

    public class JsonResponseGetForm
    {
        public bool success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public ResponseGetForm result { get; set; }

       
    }

    public class JsonResponseSaveForm
    {
        public bool success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public ResponseSaveForm result { get; set; }


       
    }

}
