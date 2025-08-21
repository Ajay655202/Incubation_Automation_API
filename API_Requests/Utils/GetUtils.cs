using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incubation_RestAPIBusLogicLayer_API.Responses;
using Incubation_RestAPIFrameworkFile.Utils;
using RestSharp;

namespace Incubation_RestAPIBusLogicLayer_API.Utils
{
    public class GetUtils
    {
        public static T GetPost<T>(string resourceUrl)

        {
            return RestClientUtils.Get<T>(resourceUrl);
        }
    }
}
