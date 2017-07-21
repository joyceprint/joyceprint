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
            const string expectedRecaptchaSecretKey = "6LcC2Q0UAAAAALvPAkBtQT2a5AE8DUCotVfQu04t";

            AssertAreEqual(expectedRecaptchaSecretKey, Config.RecaptchaSecretKey, "Recaptcha Secret Key");
        }

        /// <summary>
        /// This method will test the function that retrieves the Recaptcha Public Key
        /// </summary>
        [TestMethod]
        public void GetRecaptchaPublicKeyTest()
        {
            const string expectedRecaptchaPublicKey = "6LcC2Q0UAAAAADtadrrG_FTRs82tvd2J1fOwK-KW";

            AssertAreEqual(expectedRecaptchaPublicKey, Config.RecaptchaPublicKey, "Recaptcha Public Key");
        }

        /// <summary>
        /// This method will test the function that retrieves the Notification Header Success text
        /// </summary>
        [TestMethod]
        public void GetNotificationHeaderSuccessTest()
        {
            const string expectedNotificationHeaderSuccess = "We have recieved your email and will get back to you shortly";

            AssertAreEqual(expectedNotificationHeaderSuccess, Config.NotificationHeaderSuccess, "Notification Header Success");
        }

        /// <summary>
        /// This method will test the function that retrieves the Notification Message Success text
        /// </summary>
        [TestMethod]
        public void GetNotificationMessageSuccessTest()
        {
            const string expectedNotificationMessageSuccess = "Some body text";

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