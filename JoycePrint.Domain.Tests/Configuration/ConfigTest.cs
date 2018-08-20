using JoycePrint.Domain.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests.Configuration
{
    [TestClass]
    public class ConfigTest : BaseTest
    {
        /// <summary>
        /// This method will test the function that retrieves the Recaptcha Url
        /// </summary>
        [TestMethod]
        public void GetRecaptchaUrlTest()
        {
            const string expectedRecaptchaUrl = "https://www.google.com/recaptcha/api/siteverify";

            AssertAreEqual(expectedRecaptchaUrl, Config.RecaptchaUrl, "Recaptcha Url");
        }

        /// <summary>
        /// This method will test the function that retrieves the Recaptcha Secret Key
        /// </summary>
        [TestMethod]
        public void GetRecaptchaSecretKeyTest()
        {
            const string expectedRecaptchaSecretKey = "6Lf8OyIUAAAAAKBAsgBMK_2wC_LPRdP4ltvGvOnc";

            AssertAreEqual(expectedRecaptchaSecretKey, Config.RecaptchaSecretKey, "Recaptcha Secret Key");
        }

        /// <summary>
        /// This method will test the function that retrieves the Recaptcha Public Key
        /// </summary>
        [TestMethod]
        public void GetRecaptchaPublicKeyTest()
        {
            const string expectedRecaptchaPublicKey = "6Lf8OyIUAAAAAAg4gTKwaW6VvPvON8GIsTp_QwAE";

            AssertAreEqual(expectedRecaptchaPublicKey, Config.RecaptchaPublicKey, "Recaptcha Public Key");
        }

        /// <summary>
        /// This method will test the function that retrieves the Notification Header Success text
        /// </summary>
        [TestMethod]
        public void GetNotificationHeaderSuccessTest()
        {
            const string expectedNotificationHeaderSuccess = "We have received your email and will get back to you shortly";

            AssertAreEqual(expectedNotificationHeaderSuccess, Config.NotificationHeaderSuccess, "Notification Header Success");
        }

        /// <summary>
        /// This method will test the function that retrieves the Notification Message Success text
        /// </summary>
        [TestMethod]
        public void GetNotificationMessageSuccessTest()
        {
            const string expectedNotificationMessageSuccess = "Thank you for your enquiry";

            AssertAreEqual(expectedNotificationMessageSuccess, Config.NotificationMessageSuccess, "Notification Messge Success");
        }

        /// <summary>
        /// This method will test the function that retrieves the Notification Header Error text
        /// </summary>
        [TestMethod]
        public void GetNotificationHeaderErrorTest()
        {
            const string expectedNotificationHeaderError = "An unexpected error has occurred";

            AssertAreEqual(expectedNotificationHeaderError, Config.NotificationHeaderError, "Notification Header Error");
        }

        /// <summary>
        /// This method will test the function that retrieves the Notification Message Error text
        /// </summary>
        [TestMethod]
        public void GetNotificationMessageErrorTest()
        {
            const string expectedNotificationMessageError = "As the site is experiencing issues at the moment, please give us a call on +353-94-925-6876";

            AssertAreEqual(expectedNotificationMessageError, Config.NotificationMessageError, "Notification Message Error");
        }
    }
}