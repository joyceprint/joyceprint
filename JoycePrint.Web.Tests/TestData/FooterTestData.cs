using System;

namespace JoycePrint.Web.Tests.TestData
{
    public class FooterTestData
    {
        /// <summary>
        /// The copyright text that appears on the footer control
        /// </summary>
        public string CopyrightText => $"© JoycePrint Ltd {DateTime.Now.Year}, All rights reserved";
    }
}
