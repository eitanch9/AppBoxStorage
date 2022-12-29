using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxDAL
{
    public class ConfigData
    {
        public int Limit { get; set; }
    }




    public class Configuration
    {
        public ConfigData Data { get; set; }

        public Configuration()
        {
            var currentDir = Environment.CurrentDirectory;
            var fileName = "Data.json";
            var configPath = Path.Combine(currentDir, fileName);
            var raw = File.ReadAllText(configPath);
            Data = JsonConvert.DeserializeObject<ConfigData>(raw);
        }
    }
}
