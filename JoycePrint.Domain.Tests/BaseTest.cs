using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests
{
    public class BaseTest
    {
        protected static void AssertAreEqual<T>(T expected, T actual, string field)
        {
            Assert.AreEqual(expected, actual, $"The expected {field} value {expected} does not match the actual {field} value {actual}");
        }
    }
}
