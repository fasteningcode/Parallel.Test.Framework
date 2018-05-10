using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Parallel.Test.Framework.Base.TestSettings;
using Parallel.Test.Framework.Constants;

namespace Parallel.Test.Framework.Base.Environment.RunTests {
    public class RunTests {
        private Dictionary<string, string> TestSettings { get; set; }

        public bool CreateAllBatchFilesToRunTest(string asssemblyDll, List<string> testCategory) {
            var configsSuite = new ConfigsBeforeEachTestSuite();
            TestSettings = configsSuite.TestSetup(ExecutionAssembly.Directory + ResourceConstants.SETTINGSPATH + "/TestSettings.json");

            var batchFileDirectory = TestSettings["BATCH_FILE_DIRECTORY"];
            var solutionName = TestSettings["SOLUTION_PATH"];
            var msBuildPath = TestSettings["MS_BUILD_PATH"];
            var nunitPath = TestSettings["NUNIT_PATH"];
            var config = TestSettings["CONFIGURATION"];
            var platform = TestSettings["PLATFORM"];

            //Console.WriteLine(TestSettings["BUILD"]);
            testCategory.Add(asssemblyDll);

            foreach (var testcategoryName in testCategory) {
                Console.WriteLine(testcategoryName);
                var categoryName = testcategoryName;

                if (!Directory.Exists(batchFileDirectory))
                    Directory.CreateDirectory(batchFileDirectory);

                var batFileName = categoryName;
                var path = batchFileDirectory + "/" + batFileName + ".bat";
                if (File.Exists(path))
                    File.Delete(path); // Deleting the existing .bat file if it exists
                var f2 = File.Create(path); // Creating a new path
                f2.Close();


                //The code to write data into the batch file. 
                using (var w = new StreamWriter(path)) {
                    w.WriteLine("@echo on");
                    w.WriteLine("title " + "AutoFrame Running Test " + categoryName);
                    w.WriteLine("echo AutoFrame");
                    if (Convert.ToBoolean(TestSettings["BUILD"])) {
                        w.WriteLine("echo Clean the Solution");
                        w.WriteLine("\"" + msBuildPath + "\" " + "\"" + solutionName + "\"" + " /t:clean");

                        w.WriteLine("echo Build the Solution");
                        w.WriteLine("\"" + msBuildPath + "\" " + "\"" + solutionName + "\"" + " /t:Build /p:Configuration=" + config + " /p:Platform=\"" + platform + "\"");
                    }

                    w.WriteLine("echo N-unit Path");
                    w.WriteLine("path " + "%path%;" + nunitPath);
                    //w.WriteLine("path");

                    w.WriteLine("echo start nunit3-console.exe");
                    w.WriteLine("start nunit3-console.exe");

                    w.WriteLine("echo Execution DLL and Category with params");

                    var dllPath = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase); //Assembly.GetExecutingAssembly().ToString();
                    var executionPath = Path.GetDirectoryName(Uri.UnescapeDataString(dllPath.Path));
                    w.WriteLine(@"CD " + executionPath);
                    if (testcategoryName == asssemblyDll)
                        w.WriteLine("nunit3-console " + asssemblyDll); //; + " --where " + "\"cat ==" + categoryName + "\"");
                    else
                        w.WriteLine("nunit3-console " + asssemblyDll + " --where " + "\"cat ==" + categoryName + "\"");

                    w.Close();
                }
            }
            return true;

        }
    }
}