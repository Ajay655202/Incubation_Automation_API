using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incubation_RestAPIBusLogicLayer_API.Requests;
using Incubation_RestAPIBusLogicLayer_API.Responses;
using Incubation_RestAPIFrameworkFile.Utils;
using Newtonsoft.Json;
using RestSharp;

namespace Incubation_RestAPIBusLogicLayer_API.Utils
{
    public class PostUtils
    {
        public static CreatePostResponse CreatePost(int userId, int id, string resourceUrl)
        {
            return RestClientUtils.Post<CreatePostResponse>
                  (resourceUrl, CreatePayload(userId, id));
        }

        public static RestResponse CreatePostForStatus(int userId, int id, string resourceUrl)
        {
            return RestClientUtils.PostForStatus(resourceUrl, CreatePayload(userId, id));
        }

        public static RestResponse CreatePut(int userId, int id, string resourceUrl)
        {
            return RestClientUtils.Put(resourceUrl, CreatePayload(userId, id, title: "Modified"));
        }

        public static RestResponse CreatePatch(string resourceUrl)
        {
            return RestClientUtils.Patch(resourceUrl, CreatePayloadForPatch("Modified body"));
        }

        public static string CreatePayload(int userId = 0, int id = 0, string title = "Title for : ", string body = "Body for : ")
        {
            CreatePostValidRequests request = new CreatePostValidRequests();
            request.userId = userId;
            request.id = id;
            request.title = title + userId;
            request.body = body + userId;

            string payload = JsonConvert.SerializeObject(request);
            return payload;
        }

        public static string CreatePayloadForPatch(string body)
        {
            CreatePatchValidRequests request = new CreatePatchValidRequests();
            request.body = body;
            string payload = JsonConvert.SerializeObject(request);
            return payload;
        }

        public static bool DeletePost(int id)
        {
            return RestClientUtils.Delete("posts/" + id.ToString(), DataFormat.Json, System.Net.HttpStatusCode.OK);
        }
    }
}
