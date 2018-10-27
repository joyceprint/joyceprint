using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JoycePrint.Web.Tests.Helpers
{
    public static class AssertHelper
    {
        /// <summary>
        /// Asserts that the expected and actual values match
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The actual string</param>
        /// <param name="field">The field being tested</param>
        [Obsolete("Use Extensions MatchesActual Method")]
        public static void AssertAreEqual(string expected, string actual, string field)
        {
            Assert.AreEqual(expected, actual, $"The expected {field} text [{expected}] differs from the actual {field} text [{actual}]");
        }
    }
}