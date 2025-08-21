using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incubation_RestAPIBusLogicLayer_API.Responses
{
    public class GetPostResponse
    {
        public int userId {  get; set; }
        public int id {  get; set; }
        public string title {  get; set; }
        public string body {  get; set; }
    }
    
    public class GetCommentsResponse
    {
        public int postId {  get; set; }
        public int id {  get; set; }
        public string name {  get; set; }
        public string email {  get; set; }
        public string body {  get; set; }
    }
}
