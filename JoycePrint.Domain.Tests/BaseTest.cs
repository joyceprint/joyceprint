using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests
{
    [TestClass]
    public class BaseTest
    {
        /// <summary>
        /// This method will run when the test assembly is initialized
        /// </summary>
        /// <param name="context"></param>
        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext context)
        {
            Console.WriteLine("Assembly Initializing...");            
        }

        protected static void AssertAreEqual<T>(T expected, T actual, string field)
        {
            Assert.AreEqual(expected, actual, $"The expected [{field}] value [{expected}] does not match the actual [{field}] value [{actual}]");
        }
    }
}