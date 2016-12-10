using Microsoft.VisualStudio.TestTools.UnitTesting;
using JoycePrint.Domain.Configuration;
using JoycePrint.Domain.Business;
using System.Net.Configuration;
using System.Net.Mail;

namespace JoycePrint.Domain.Tests
{
    [TestClass]
    public class EmailTest : BaseTest
    {
        /// <summary>
        /// This is the To Email Address that will be used to test the working email
        /// </summary>
        private readonly string EmailToAddress = "polydegmon@gmail.com";

        /// <summary>
        /// Test the smtpConfig is return the correct settings
        /// </summary>
        [TestMethod]
        public void GetSmtpConfig()
        {
            IEmail Email = new Email();

            var actualSmtpConfig = Email.smtpConfig;

            var expectedSmptConfig = new SmtpSection();
            expectedSmptConfig.From = "some@email.com";
            expectedSmptConfig.Network.Host = "myhost";
            expectedSmptConfig.Network.Port = 25;
            expectedSmptConfig.Network.UserName = "";
            expectedSmptConfig.Network.Password = "";

            AssertAreEqual(expectedSmptConfig.From, actualSmtpConfig.From, "smtp FROM setting");
            AssertAreEqual(expectedSmptConfig.Network.Host, actualSmtpConfig.Network.Host, "smtp HOST setting");
            AssertAreEqual(expectedSmptConfig.Network.Port.ToString(), actualSmtpConfig.Network.Port.ToString(), "smtp PORT setting");
            AssertAreEqual(expectedSmptConfig.Network.UserName, actualSmtpConfig.Network.UserName, "smtp USERNAME setting");
            AssertAreEqual(expectedSmptConfig.Network.Password, actualSmtpConfig.Network.Password, "smtp PASSWORD setting");
        }

        /// <summary>
        /// Test the SendEmail function by passing it a bad mail message object
        /// </summary>
        [TestMethod]
        public void SendEmailBadMailMessageTest()
        {
            IEmail Email = new Email();

            var expectedEmailSent = false;

            var actualEmailSent = Email.SendEmail(null, Email.smtpConfig);

            AssertAreEqual(expectedEmailSent, actualEmailSent, "Email Sent Flag"); 
        }

        /// <summary>
        /// Test the SendEmail function by passing it a bad smtp section object
        /// </summary>
        [TestMethod]
        public void SendEmailBadSmptSectionTest()
        {
            IEmail Email = new Email();

            var expectedEmailSent = false;
            var mailMessage = new MailMessage();

            var actualEmailSent = Email.SendEmail(mailMessage, null);

            AssertAreEqual(expectedEmailSent, actualEmailSent, "Email Sent Flag");
        }

        /// <summary>
        /// Test the SendEmail function by passing it a good set of objects
        /// </summary>
        [TestMethod]
        public void SendEmailGoodTest()
        {
            IEmail Email = new Email();
            
            var expectedEmailSent = true;

            var mailMessage = new MailMessage(Email.smtpConfig.From, EmailToAddress);
            var smptConfig = Email.smtpConfig;

            var actualEmailSent = Email.SendEmail(mailMessage, smptConfig);

            AssertAreEqual(expectedEmailSent, actualEmailSent, "Email Sent Flag");
        }
    }
}
