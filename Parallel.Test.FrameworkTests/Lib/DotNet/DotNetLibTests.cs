using NUnit.Framework;
using Parallel.Test.Framework.Lib.DotNet;
//using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Parallel.Test.FrameworkTests.Lib.DotNet
{
    
    public class DotNetLibTests
    {
#if (!DEBUG)
        [Test]
#endif
        public void IsNullOrEmptyTest() {
            string s1 = "NULL"; Assert.True(s1.IsNullOrEmpty());
            s1 = "null"; Assert.IsTrue(s1.IsNullOrEmpty());
            s1 = ""; Assert.True(s1.IsNullOrEmpty());
            s1 = null; Assert.True(s1.IsNullOrEmpty());
            s1 = " "; Assert.False(s1.IsNullOrEmpty());

        }
    }
}