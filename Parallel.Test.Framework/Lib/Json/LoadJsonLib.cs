using System.IO;
using Newtonsoft.Json.Linq;

namespace Parallel.Test.Framework.Lib.Json {
    public class LoadJsonLib
    {
        public JObject LoadJson(string filePath)
        {
            string json;
            using (var r = new StreamReader(filePath))
            {
                json = r.ReadToEnd();
            }

            var sourceObject = JObject.Parse(json);
            return sourceObject;
        }

        public  string ReadFile(string filePath)
        {
            string readFile;
            using (var r = new StreamReader(filePath))
            {
                readFile = r.ReadToEnd();
            }

            return readFile;
        }
    }
}
