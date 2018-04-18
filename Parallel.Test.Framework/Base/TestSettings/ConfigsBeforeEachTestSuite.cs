using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft.Json.Linq;
using Parallel.Test.Framework.Constants;
using Parallel.Test.Framework.Lib.Json;
using Parallel.Test.FrameworkTests;

namespace Parallel.Test.Framework.Base.TestSettings {
    public class ConfigsBeforeEachTestSuite {
        public Dictionary<string, string> ReadEnvironmentFromJson(string envJsonPath, string envName) {
            var data = new Dictionary<string, string>();

            var jsonLib = new LoadJsonLib();
            var o = jsonLib.LoadJson(envJsonPath);
            var testCaseDetails = o.SelectToken("$.TestEnv[?(@.EnvName == '" + envName + "')]");
            //JToken testDataDetails = testCaseDetails.SelectToken("$.TestRow[?(@.RowNumber == '" + testDataNo + "')]");
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

        public Dictionary<string, string> TestSetup(string testSettingsPath) {

            var data = new Dictionary<string, string>();
            try {
                var jsonLib = new LoadJsonLib();
                var o = jsonLib.LoadJson(testSettingsPath);
                var selectToken = o.SelectToken("$.SETUP");

                var abc = selectToken.ToList();
                foreach (var a in abc) {
                    Console.WriteLine(a.Type);
                    if (a.Type == JTokenType.Object) {
                        var obj = a.ToObject<Dictionary<string, string>>();
                        foreach (var pair in obj)
                            data.Add(pair.Key, pair.Value);
                    }
                    else if (a.Type == JTokenType.Property) {
                        try {
                            var item = a.ToList();
                            foreach (var i in item) {
                                Console.WriteLine(i.Key + );
                            }
                        }
                        catch (Exception) {
                        }
                    }
                }

                return data;
            }
            catch (Exception e) {
                Console.WriteLine("Error in Reading from TestSettings.json file" + e);
            }

            return data;
        }
    }
}