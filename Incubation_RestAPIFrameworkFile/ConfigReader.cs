using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Incubation_RestAPIFrameworkFile
{
    public class Config
    {
        public string? BaseUrl { get; set; }
        public string? SubmitJokeUrl { get; set; }
        public string? SubmitJokeNon200Url { get; set; }
        public string? PostUrl { get; set; }
        public string? CommentsUrl { get; set; }
        public string? CommentsPostIdUrl { get; set; }
    }

    public static class ConfigReader
    {
        private static Config? _config;

        public static Config Settings
        {
            get
            {
                if (_config == null)
                {
                    string json = File.ReadAllText("appsettings.json");
                    _config = JsonConvert.DeserializeObject<Config>(json);
                }
                return _config;
            }
        }
    }
}
