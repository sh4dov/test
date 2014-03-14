using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        [TestInitialize]
        public void Initialize1()
        {
            Debugger.Break();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Debugger.Break();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    public class BaseTest
    {
        [TestInitialize]
        public void Initialize2()
        {
            Debugger.Break();
        }

        [TestCleanup]
        public void TestCleanup2()
        {
            Debugger.Break();
        }
    }
}