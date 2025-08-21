using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Incubation_RestAPIFrameworkFile.Utils
{
    public class RestClientUtils
    {
        static RestClient _RestClient;
        static RestRequest _RestRequest;

        private static RestClient RestClient
        {
            get
            {
                if (_RestClient == null)
                {
                    return new RestClient(ConfigReader.Settings.BaseUrl);
                }
                else
                {
                    return _RestClient;
                }
            }
        }

        private static RestClient RestClientWithAuthentication(string username, string password)
        {
            var options = new RestClientOptions(ConfigurationManager.AppSettings["BaseUrl"].ToString())
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
            return new RestClient(options);
        }

        public static RestRequest CreateRequest(string resource, Method method)
        {
            if (_RestRequest == null)
            {
                RestRequest req = new RestRequest(resource, method);
                req.AddHeader("Accept", "application/json");
                req.AddHeader("Connection", "keep-alive");
                return req;
            }
            else
            {
                return _RestRequest;
            }
        }

        public static RestRequest GetResponse(string resource, Method method, DataFormat dataFormat)
        {
            if (_RestRequest == null)
            {
                RestRequest req = new RestRequest(resource, method);
                req.AddHeader("Accept", "application/json");
                req.AddHeader("Connection", "keep-alive");
                return req;
            }
            else
            {
                return _RestRequest;
            }
        }

        //Create
        public static T Post<T>(string resource, string payload)
        {
            return JsonConvert.DeserializeObject<T>(
                RestClient.Execute
                (
                    CreateRequest(resource, Method.Post)?
                    .AddBody(payload)
                    )?.Content);
        }

        public static RestResponse PostForStatus(string resource, string payload)
        {
            return RestClient.Execute
                    (
                    CreateRequest(resource, Method.Post)?
                    .AddBody(payload)
                    );
        }

        public static RestResponse Put(string resource, string payload)
        {
            return 
                RestClient.Execute
                (
                    CreateRequest(resource, Method.Put)?
                    .AddBody(payload)
                    );
        }
        
        public static RestResponse Patch(string resource, string payload)
        {
            return 
                RestClient.Execute
                (
                    CreateRequest(resource, Method.Patch)?
                    .AddBody(payload)
                    );
        }

        public static T Get<T>(string resource)
        {
            var res = RestClient.Execute(CreateRequest(resource, Method.Get));

            return JsonConvert.DeserializeObject<T>(
                RestClient.Execute
                (
                    CreateRequest(resource, Method.Get)
                ).Content);
        }

        //Autherization
        public static T Post<T>(string resource, string payload, DataFormat dataFormat, string username, string password)
        {
            return JsonConvert.DeserializeObject<T>(
                RestClientWithAuthentication(username, password).Execute
                (
                    CreateRequest(resource, Method.Post)
                    .AddBody(payload)
                    ).Content);
        }

        //With Headers
        public static T Post<T>(string resource, string payload, DataFormat dataFormat, List<KeyValuePair<string, string>> headers)
        {
            return JsonConvert.DeserializeObject<T>(
                RestClient.Execute
                (
                    CreateRequest(resource, Method.Post, dataFormat, headers)
                    .AddBody(payload)
                    ).Content);
        }

        public static RestRequest CreateRequest(string resource, Method method, DataFormat dataFormat, List<KeyValuePair<string, string>> headers)
        {
            foreach (var item in headers)
            {
                _RestRequest.AddHeader(item.Key, item.Value);
            }
            return _RestRequest;
        }

        //Delete
        public static bool Delete(string resource, DataFormat dataFormat, HttpStatusCode expectedStatusCode)
        {
            return RestClient.Execute
            (
                CreateRequest(resource, Method.Delete)
            ).StatusCode.Equals(expectedStatusCode);
        }
    }
}
