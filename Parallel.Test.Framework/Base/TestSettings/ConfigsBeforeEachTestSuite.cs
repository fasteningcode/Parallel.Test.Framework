using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json.Linq;
using Parallel.Test.Framework.Constants;
using Parallel.Test.Framework.Lib.Json;

namespace Parallel.Test.Framework.Base.TestSettings {
    public class ConfigsBeforeEachTestSuite {
        public Dictionary<string, string> ReadEnvironmentFromJson(string envJsonPath, string envName) {

            var value = "{\r\n\t\"TestEnv\": [{\r\n\t\t\t\"EnvName\": \"qa\",\r\n\t\t\t\"EnvDetails\": [{\r\n\t\t\t\t\"DbConnStr\": \"Data Source=sql02;User id=aadhi;Password=Secret;\",\r\n\t\t\t\t\"FrontEnd2\":\"https://www.google.com/\",\r\n\t\t\t\t\"BackEndTest\": \"https://fasteningcode.local/backend\"\r\n\t\t\t}]\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"EnvName\": \"UAT\",\r\n\t\t\t\"EnvDetails\": [{\r\n\t\t\t\t\"FrontEnd\": \"http://fasteningcode.com/\",\r\n\t\t\t\t\"FrontEnd2\":\"https://www.google.com/\",\r\n\t\t\t\t\"DbConnUnsecured\": \"Data Source=sql03;User id=Aadhi;Password=Secret; Initial Catalog = Catalog8055;\"\r\n\t\t\t}]\r\n\t\t}\r\n\t]\r\n}";
            Create_TestSettingsFileIfItDidntExists(envJsonPath, value);

            var data = new Dictionary<string, string>();

            var jsonLib = new JsonLib();
            var o = jsonLib.JObject(envJsonPath);
            var testCaseDetails = o.SelectToken("$.TestEnv[?(@.EnvName == '" + envName + "')]");
            var testData = testCaseDetails.SelectToken("$.EnvDetails");
            var abc = testData.ToList();
            foreach (var a in abc)
                //Console.WriteLine(a.Type);
                if (a.Type == JTokenType.Object) {
                    var obj = a.ToObject<Dictionary<string, string>>();
                    foreach (var pair in obj)
                        data.Add(pair.Key, pair.Value);
                }
            return data;
        }

        public Dictionary<string, string> TestSetup(string testSettingsPath)
        {
            var value = "{\r\n\t\"SETUP\": {\r\n\t\t\r\n\t\t// START DONT CHANGE THE KEY BELOW\r\n\t\t\"ENVIRONMENT\": \"UAT\",\r\n\t\t//Available - LOCAL,SAUCE\r\n\t\t\"RUN_TEST\": \"LOCAL\",\r\n\t\t\"BROWSER\": \"CHROME\",\r\n\t\t\"VERSION\": \"45\",\r\n\t\t\"OS\": \"WINDOWS 7\",\r\n\t\t\"DEVICENAME\": \"\",\r\n\t\t\"DEVICEORIENTATION\": \"\",\r\n\t\t\"USERNAME_SAUCE\": \"aadhithbose\",\r\n\t\t\"ACCESS_KEY_SAUCE\": \"1cc813ac-bec2-4dd8-9e9e-a239ec2e7c2c\",\r\n\t\t\"WAIT_SEC\": \"180\",\r\n\t\t\"TIMEOUT_MIN\": \"3\",\r\n\t\t\"BATCH_FILE_DIRECTORY\": \"C:/TESTSUITE\",\r\n\t\t\"TEMP_FOLDER\": \"C:/TEMP_FOLDER\",\r\n\t\t\"MS_BUILD_PATH\": \"C:/PROGRAM FILES (X86)/MSBUILD/14.0/BIN/MSBUILD.EXE\",\r\n\t\t\"SOLUTION_NAME\": \"Parallel.Test.Framework.sln\",\r\n\t\t\"NUNIT_PATH\": \"/PACKAGES/NUNIT.CONSOLERUNNER.3.8.0/TOOLS\"\r\n\t\t// STOP DONT CHANGE THE KEY ABOVE\r\n\t\t// Can Create any other Key / Value Below\r\n\t}\r\n}\r\n";
            Create_TestSettingsFileIfItDidntExists(testSettingsPath, value);
            var data = new Dictionary<string, string>();
            try
            {
                var jsonLib = new JsonLib();
                var o = jsonLib.JObject(testSettingsPath);
                var testData = o.SelectToken("$.SETUP");
                var abc = testData.ToObject<Dictionary<string, string>>();
                Console.WriteLine(@"Test Settings>>Setup Data Count" + abc.Count);
                foreach (var pair in abc)
                {
                    //Console.WriteLine(pair.Key + "" + pair.Value);
                    data.Add(pair.Key, pair.Value);
                }

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in Reading from TestSettings.json file" + e);
            }

            return data;
        }

        private static void Create_TestSettingsFileIfItDidntExists(string path, string value) {

            if (!Directory.Exists(Assembly.Directory + ResourceConstants.SETTINGSPATH))
                Directory.CreateDirectory(Assembly.Directory + ResourceConstants.SETTINGSPATH);



            if (!File.Exists(path))
            {
                var f2 = File.Create(path);
                f2.Close();
                using (var w = new StreamWriter(path))
                {
                    w.WriteLine(value);
                    w.Close();
                }
            }
        }
    }
}