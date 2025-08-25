using System.Net;
using Incubation_RestAPIBusLogicLayer_API.Responses;
using Incubation_RestAPIBusLogicLayer_API.Utils;
using Incubation_RestAPIFrameworkFile;
using RestSharp;
using Incubation_Automation_API_Parallel;
using System.ComponentModel;

[assembly: Parallelize(Workers = 6, Scope = ExecutionScope.MethodLevel)]
namespace Incubation_Automation_API
{
    [TestClass]
    public class API_Tests
    {
        [TestMethod]
        [Category("Parallel")]
        public void GetPost()
        {
            GetPostResponse[] response = GetUtils.GetPost<GetPostResponse[]>(ConfigReader.Settings.PostUrl);
            GlobalData.FirstId = response[0].id;
            GlobalData.LastId = response.Last().id;
            GlobalData.LastUserId = response.Last().userId;
            if (GlobalData.FirstId.Equals(1))
            {
                Console.WriteLine("Got response successfully");
            }
            else
            {
                Assert.Fail("Failed to Get response");
            }
        }

        [TestMethod]
        [Category("Parallel")]
        public void GetPostWithUserId()
        {
            GetPostResponse response = GetUtils.GetPost<GetPostResponse>(string.Concat(ConfigReader.Settings.PostUrl, "/", GlobalData.FirstId));
            if (response.userId.Equals(GlobalData.FirstId))
            {
                Console.WriteLine($"Got response with user id {response.userId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to Get response for the user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        public void GetPostWithUserIdError()
        {
            GetPostResponse response = GetUtils.GetPost<GetPostResponse>(string.Concat(ConfigReader.Settings.PostUrl, "!@#$/", GlobalData.FirstId));
            if (response.userId.Equals(GlobalData.FirstId))
            {
                Console.WriteLine($"Got response with user id {response.userId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to Get response for the user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        [Category("Parallel")]
        public void GetPostWithUserIdComments()
        {
            string url = string.Concat(ConfigReader.Settings.PostUrl, "/", GlobalData.FirstId, ConfigReader.Settings.CommentsUrl);
            GetCommentsResponse[] response = GetUtils.GetPost<GetCommentsResponse[]>(url);
            if (response[0].postId.Equals(GlobalData.FirstId))
            {
                Console.WriteLine($"Got response of comments with user id {GlobalData.FirstId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to Get response of comments for the user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        [Category("Parallel")]
        public void GetPostWithCommentsPostId()
        {
            string url = string.Concat(ConfigReader.Settings.CommentsPostIdUrl, GlobalData.FirstId);
            GetCommentsResponse[] response = GetUtils.GetPost<GetCommentsResponse[]>(url);
            if (response[0].postId.Equals(GlobalData.FirstId))
            {
                Console.WriteLine($"Got response of comments with user id {GlobalData.FirstId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to Get response of comments for the user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        public void PostData()
        {
            RestResponse response = PostUtils.CreatePostForStatus(GlobalData.LastId + 1, GlobalData.LastUserId + 1, ConfigReader.Settings.PostUrl);
            if (response.StatusCode.Equals(HttpStatusCode.Created))
            {
                Console.WriteLine($"Post with user id {GlobalData.LastUserId} sent successfully");
            }
            else
            {
                Assert.Fail($"Failed to send post with user id {GlobalData.LastUserId}");
            }
        }

        [TestMethod]
        public void PostDataError()
        {
            string url = string.Concat(ConfigReader.Settings.PostUrl, "!@#$%");
            RestResponse response = PostUtils.CreatePostForStatus(GlobalData.LastId + 1, GlobalData.LastUserId + 1, url);
            if (response.StatusCode.Equals(HttpStatusCode.Created))
            {
                Console.WriteLine($"Post with user id {GlobalData.LastUserId} sent successfully");
            }
            else
            {
                Assert.Fail($"Failed to send post with user id {GlobalData.LastUserId}");
            }
        }

        [TestMethod]
        public void PutData()
        {
            string resourceUrl = string.Concat(ConfigReader.Settings.PostUrl, "/", GlobalData.FirstId);
            RestResponse response = PostUtils.CreatePut(GlobalData.LastId + 1, GlobalData.LastUserId + 1, resourceUrl);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                Console.WriteLine($"Executed put for user id {GlobalData.FirstId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to execute put for user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        public void PutDataError()
        {
            string resourceUrl = string.Concat(ConfigReader.Settings.PostUrl, "/", string.Concat(GlobalData.FirstId, "!@#$"));
            RestResponse response = PostUtils.CreatePut(GlobalData.LastId + 1, GlobalData.LastUserId + 1, resourceUrl);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                Console.WriteLine($"Executed put for user id {GlobalData.FirstId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to execute put for user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        public void PatchData()
        {
            string resourceUrl = string.Concat(ConfigReader.Settings.PostUrl, "/", GlobalData.FirstId);
            RestResponse response = PostUtils.CreatePatch(resourceUrl);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                Console.WriteLine($"Executed patch for user id {GlobalData.FirstId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to execute patch for user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        public void PatchDataError()
        {
            string resourceUrl = string.Concat(string.Concat(ConfigReader.Settings.PostUrl, "@!#@%!^!"), "/", string.Concat(GlobalData.FirstId, "!@#$"));
            RestResponse response = PostUtils.CreatePatch(resourceUrl);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                Console.WriteLine($"Executed patch for user id {GlobalData.FirstId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to execute patch for user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        public void DeleteData()
        {
            bool response = PostUtils.DeletePost(GlobalData.FirstId);
            if (response)
            {
                Console.WriteLine($"Executed Delete for user id {GlobalData.FirstId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to execute Delete for user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        public void DeleteDataError()
        {
            bool response = PostUtils.DeletePost(GlobalData.FirstId, resourceUrl: "WrongUrl");
            if (response)
            {
                Console.WriteLine($"Executed Delete for user id {GlobalData.FirstId} successfully");
            }
            else
            {
                Assert.Fail($"Failed to execute Delete for user id {GlobalData.FirstId}");
            }
        }

        [TestMethod]
        public void PostDataNon200()
        {
            RestResponse response = GetUtils.GetPost<RestResponse>(string.Concat(ConfigReader.Settings.PostUrl, "!@#$"));
            if (response.StatusCode == 0)
            {
                Console.WriteLine("Executed non 200 successfully");
            }
            else
            {
                Assert.Fail("Failed to execute non 200 response");
            }
        }

        //Some more negative scenarios, when all requests have to fail

        //Report
        //CICD
    }
}