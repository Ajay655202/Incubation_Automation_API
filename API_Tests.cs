using System.Net;
using Incubation_RestAPIBusLogicLayer_API.Responses;
using Incubation_RestAPIBusLogicLayer_API.Utils;
using Incubation_RestAPIFrameworkFile;
using RestSharp;
using Incubation_Automation_API_Parallel;

[assembly: Parallelize(Workers = 2, Scope = ExecutionScope.MethodLevel)]
namespace Incubation_Automation_API
{
    [TestClass]
    public class API_Tests
    {
        [TestMethod]
        public void GetPost()
        {
            Thread.Sleep(20000);
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
        public void PostWithUserId()
        {
            Thread.Sleep(20000);

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
        public void PostWithUserIdComments()
        {
            Thread.Sleep(20000);
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
        public void PostWithCommentsPostId()
        {
            Thread.Sleep(20000);
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
            Thread.Sleep(20000);
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
        public void PutData()
        {
            Thread.Sleep(20000);
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
        public void PatchData()
        {
            Thread.Sleep(20000);
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
        public void DeleteData()
        {
            Thread.Sleep(20000);
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
        public void PostDataNon200()
        {
            Thread.Sleep(20000);
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
    }
}