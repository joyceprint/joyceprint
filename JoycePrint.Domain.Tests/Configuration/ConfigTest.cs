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
            IConfig config = new Config();

            var expectedRecaptchaUrl = "https://www.google.com/recaptcha/api/siteverify";

            AssertAreEqual(expectedRecaptchaUrl, config.RecaptchaUrl, "Recaptcha Url");
        }

        /// <summary>
        /// This method will test the function that retrieves the Recaptcha Secret Key
        /// </summary>
        [TestMethod]
        public void GetRecaptchaSecretKeyTest()
        {
            IConfig config = new Config();

            const string expectedRecaptchaSecretKey = "6LcC2Q0UAAAAALvPAkBtQT2a5AE8DUCotVfQu04t";

            AssertAreEqual(expectedRecaptchaSecretKey, config.RecaptchaSecretKey, "Recaptcha Secret Key");
        }

        /// <summary>
        /// This method will test the function that retrieves the Recaptcha Public Key
        /// </summary>
        [TestMethod]
        public void GetRecaptchaPublicKeyTest()
        {
            IConfig config = new Config();

            const string expectedRecaptchaPublicKey = "6LcC2Q0UAAAAADtadrrG_FTRs82tvd2J1fOwK-KW";

            AssertAreEqual(expectedRecaptchaPublicKey, config.RecaptchaPublicKey, "Recaptcha Public Key");
        }
    }
}
