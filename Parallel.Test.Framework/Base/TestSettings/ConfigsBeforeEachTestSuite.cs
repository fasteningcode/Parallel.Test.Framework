using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Parallel.Test.Framework.Constants;
using Parallel.Test.Framework.Lib.Json;

namespace Parallel.Test.Framework.Base.TestSettings {
    public class ConfigsBeforeEachTestSuite {
        public Dictionary<string, string> ReadEnvironmentFromJson(string envJsonPath, string envName) {
            var s =
                "{\r\n\t\"TestEnv\": [\r\n\t\t{\r\n\t\t\t\"EnvName\": \"qa1\",\r\n\t\t\t\"EnvDetails\": [\r\n\t\t\t\t{\r\n\t\t\t\t\t\"FrontEnd\": \"http://qa1.fasteningcode.com\",\r\n\t\t\t\t\t\"BackEndTest\": \"https://qa1.fasteningcode.com/backend\",\r\n\t\t\t\t\t\"DbConnStr\": \"Data Source=sql02;User id=aadhi;Password=Secret;\"\r\n\t\t\t\t}\r\n\t\t\t]\r\n\t\t},{\r\n\t\t\t\"EnvName\": \"uat1\",\r\n\t\t\t\"EnvDetails\": [\r\n\t\t\t\t{\r\n\t\t\t\t\t\"FrontEnd\": \"http://uat1.fasteningcode.com\",\r\n\t\t\t\t\t\"BackEndTest\": \"https://uat1.fasteningcode.com/backend\",\r\n\t\t\t\t\t\"DbConnStr\": \"Data Source=sql02;User id=aadhi;Password=Secret;\"\r\n\t\t\t\t}\r\n\t\t\t]\r\n\t\t}\n\t]\r\n}";


            var value = s;//+ s1 + s2;
                //"{\r\n\t\"TestEnv\": [{\r\n\t\t\t\"EnvName\": \"qa1\",\r\n\t\t\t\"EnvDetails\": [{\r\n\t\t\t\t\"DbConnStr\": \"Data Source=sql02;User id=aadhi;Password=Secret;\",\r\n\t\t\t\t\"FrontEnd2\":\"https://www.google.com/\",\r\n\t\t\t\t\"BackEndTest\": \"https://fasteningcode.local/backend\"\r\n\t\t\t}]\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"EnvName\": \"UAT\",\r\n\t\t\t\"EnvDetails\": [{\r\n\t\t\t\t\"FrontEnd\": \"http://fasteningcode.com/\",\r\n\t\t\t\t\"FrontEnd2\":\"https://www.google.com/\",\r\n\t\t\t\t\"DbConnUnsecured\": \"Data Source=sql03;User id=Aadhi;Password=Secret; Initial Catalog = Catalog8055;\"\r\n\t\t\t}]\r\n\t\t}\r\n\t]\r\n}";
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
            var value = "{\r\n\t\"SETUP\": {\r\n\t\t\r\n\t\t// START DONT CHANGE THE KEY BELOW\r\n\t\t\"ENVIRONMENT\": \"uat1\",\r\n\t\t//Available - LOCAL,SAUCE\r\n\t\t\"RUN_TEST\": \"LOCAL\",\r\n\t\t\"BROWSER\": \"CHROME\",\r\n\t\t\"VERSION\": \"45\",\r\n\t\t\"OS\": \"WINDOWS 7\",\r\n\t\t\"DEVICENAME\": \"\",\r\n\t\t\"DEVICEORIENTATION\": \"\",\r\n\t\t\"USERNAME_SAUCE\": \"aadhithbose\",\r\n\t\t\"ACCESS_KEY_SAUCE\": \"1cc813ac-bec2-4dd8-9e9e-a239ec2e7c2c\",\r\n\t\t\"WAIT_SEC\": \"180\",\r\n\t\t\"TIMEOUT_MIN\": \"3\",\r\n\t\t\"BATCH_FILE_DIRECTORY\": \"C:/TESTSUITE\",\r\n\t\t\"TEMP_FOLDER\": \"C:/TEMP_FOLDER\",\r\n\t\t\"MS_BUILD_PATH\": \"C:/PROGRAM FILES (X86)/MSBUILD/14.0/BIN/MSBUILD.EXE\",\r\n\t\t\"SOLUTION_NAME\": \"Parallel.Test.Framework.sln\",\r\n\t\t\"NUNIT_PATH\": \"/PACKAGES/NUNIT.CONSOLERUNNER.3.8.0/TOOLS\"\r\n\t\t// STOP DONT CHANGE THE KEY ABOVE\r\n\t\t// Can Create any other Key / Value Below\r\n\t}\r\n}\r\n\r\n";
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
                Console.WriteLine(@"Error in Reading from TestSettings.json file" + e);
            }

            return data;
        }

        private static void Create_TestSettingsFileIfItDidntExists(string path, string value) {


            if (!Directory.Exists(ExecutionAssembly.Directory + ResourceConstants.SETTINGSPATH))
                Directory.CreateDirectory(ExecutionAssembly.Directory + ResourceConstants.SETTINGSPATH);
            
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

        public void CreateSampleDataJson(string testSettingsPath) {
            const string value = "{\r\n\t\"TestCases\": [\r\n\t\t{\r\n\t\t\t\"TestCaseId\": \"tc1\",\r\n\t\t\t\"TestName\": \"sample test ma,e\",\r\n\t\t\t\"TetDescription\": \"sample Test Description \",\r\n\t\t\t\"TestRow\": [\r\n\t\t\t\t{\r\n\t\t\t\t\t\"TestDataNumber\": \"1\",\r\n\t\t\t\t\t\"TestData\": [\r\n\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\"username\": \"john\",\r\n\t\t\t\t\t\t\t\"password\": \"john\"\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t]\r\n\t\t\t\t},{\r\n\t\t\t\t\t\"TestDataNumber\": \"2\",\r\n\t\t\t\t\t\"TestData\": [\r\n\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\"username\": \"w,im\",\r\n\t\t\t\t\t\t\t\"password\": \"wim\"\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t]\r\n\t\t\t\t}\r\n\t\t\t],\"DefaultTestData\" :  \"1\",\r\n\t\t\t\"RegressionSuite\": \"1,2,3,4\",\r\n\t\t\t\"SanitySuite\": \"1\"\r\n\t\t},{\r\n\t\t\t\"TestCaseId\": \"tc2\",\r\n\t\t\t\"TestName\": \"Test Name 2 \",\r\n\t\t\t\"TetDescription\": \"sample Test Description 2\",\r\n\t\t\t\"TestRow\": [\r\n\t\t\t\t{\r\n\t\t\t\t\t\"TestDataNumber\": \"1\",\r\n\t\t\t\t\t\"TestData\": [\r\n\t\t\t\t\t\t{\r\n\t\t\t\t\t\t\t\"updateUsername\": \"ed\",\r\n\t\t\t\t\t\t\t\"updatePassword\": \"ed\"\r\n\t\t\t\t\t\t}\r\n\t\t\t\t\t]\r\n\t\t\t\t}\r\n\t\t\t],	\t\"DefaultTestData\" :  \"1,1,1,1,1\",\r\n\t\t\t\"RegressionSuite\": \"1,2\",\r\n\t\t\t\"SanitySuite\": \"1\"\r\n\t\t}\r\n\t]\r\n\r\n}";
            Create_TestSettingsFileIfItDidntExists(testSettingsPath, value);
        }

        public void CreateSampleDataCsv(string testSettingsPath) {
            const string value = "TestCaseId,TestName,TetDescription,DefaultTestData,RegressionSuite,SanitySuite,TestDataNumber,username,password,updateUsername,updatePassword\r\ntc1,\"sample test ma,e\",sample Test Description ,1,\"1,2,3,4\",1,1,john,john,,\r\n,,,,,,,\"w,im\",wim,,\r\ntc2,Test Name 2 ,sample Test Description 2,\"1,1,\",\"1,2\",1,1,,,ed,ed\r\n";
            Create_TestSettingsFileIfItDidntExists(testSettingsPath, value);
        }
    }
}