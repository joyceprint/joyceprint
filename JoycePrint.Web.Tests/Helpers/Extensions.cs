using JoycePrint.Web.Tests.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Web.Tests.Helpers
{    
    static class Extensions
    {
        public static string UpdateCssTo(this string value, FieldCss getFor)
        {
            switch (getFor)
            {
                case FieldCss.Initial:
                    return value;
                default:
                    return value;
            }
        }

        /// <summary>
        /// Assert Extension to replace the AssertHelper.AssertAreEqual method
        /// </summary>
        /// <param name="actual">The object the method is called on</param>
        /// <param name="expected">The expected value</param>
        /// <param name="field">The field being asserted on</param>
        public static void AssertAreEqual(this string actual, string expected, string field)
        {
            Assert.AreEqual(expected, actual, $"The expected {field} text [{expected}] differs from the actual {field} text [{actual}]");
        }
    }
}