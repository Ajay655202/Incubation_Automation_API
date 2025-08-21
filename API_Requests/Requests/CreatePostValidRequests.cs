using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incubation_RestAPIBusLogicLayer_API.Requests
{
    internal class CreatePostValidRequests
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    internal class CreatePatchValidRequests
    {
        public string body { get; set; }
    }
}
