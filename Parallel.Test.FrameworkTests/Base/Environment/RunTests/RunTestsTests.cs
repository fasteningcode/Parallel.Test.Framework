using System.Collections.Generic;
using NUnit.Framework;


namespace Parallel.Test.FrameworkTests.Base.Environment.RunTests
{
    
    public class RunTestsTests : Framework.Base.Base
    {
#if(!DEBUG)
        [Test]
#endif
        public void CreateAllBatchFilesToRunTestTest() {

            var testCategory = new List<string> {"UnitTest"};

            var c = new Framework.Base.Environment.RunTests.RunTests();
            var codeExit = c.CreateAllBatchFilesToRunTest("Parallel.Test.FrameworkTests.dll", testCategory);

            Assert.IsTrue(codeExit);
        }
    }
}