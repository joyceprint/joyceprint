//using System.Net.Configuration;
//using System.Net.Mail;
//using JoycePrint.Domain.Mail;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace JoycePrint.Domain.Tests.Mail
//{
//    [TestClass]
//    public class EmailTest : BaseTest
//    {
//        /// <summary>
//        /// This is the To Email Address that will be used to test the working email
//        /// </summary>
//        private const string EmailToAddress = "polydegmon@gmail.com";

//        /// <summary>
//        /// Test the SmtpConfig is return the correct settings
//        /// </summary>
//        [TestMethod]
//        public void GetSmtpConfig()
//        {
//            IEmail email = new Email();

//            var actualSmtpConfig = email.SmtpConfig;

//            var expectedSmptConfig = new SmtpSection {From = "some@email.com"};
//            expectedSmptConfig.Network.Host = "myhost";
//            expectedSmptConfig.Network.Port = 25;
//            expectedSmptConfig.Network.UserName = null;
//            expectedSmptConfig.Network.Password = null;

//            AssertAreEqual(expectedSmptConfig.From, actualSmtpConfig.From, "smtp FROM setting");
//            AssertAreEqual(expectedSmptConfig.Network.Host, actualSmtpConfig.Network.Host, "smtp HOST setting");
//            AssertAreEqual(expectedSmptConfig.Network.Port.ToString(), actualSmtpConfig.Network.Port.ToString(), "smtp PORT setting");
//            AssertAreEqual(expectedSmptConfig.Network.UserName, actualSmtpConfig.Network.UserName, "smtp USERNAME setting");
//            AssertAreEqual(expectedSmptConfig.Network.Password, actualSmtpConfig.Network.Password, "smtp PASSWORD setting");
//        }

//        /// <summary>
//        /// Test the SendEmail function by passing it a bad mail message object
//        /// </summary>
//        [TestMethod]
//        public void SendEmailBadMailMessageTest()
//        {
//            IEmail email = new Email();

//            const bool expectedEmailSent = false;

//            var actualEmailSent = email.SendEmail(null, email.SmtpConfig);

//            AssertAreEqual(expectedEmailSent, actualEmailSent, "Email Sent Flag"); 
//        }

//        /// <summary>
//        /// Test the SendEmail function by passing it a bad smtp section object
//        /// </summary>
//        [TestMethod]
//        public void SendEmailBadSmptSectionTest()
//        {
//            IEmail email = new Email();

//            const bool expectedEmailSent = false;
//            var mailMessage = new MailMessage();

//            var actualEmailSent = email.SendEmail(mailMessage, null);

//            AssertAreEqual(expectedEmailSent, actualEmailSent, "Email Sent Flag");
//        }

//        /// <summary>
//        /// Test the SendEmail function by passing it a good set of objects
//        /// </summary>
//        [TestMethod]        
//        public void SendEmailGoodTest()
//        {
//            IEmail email = new Email();

//            const bool expectedEmailSent = true;

//            var mailMessage = new MailMessage(email.SmtpConfig.From, EmailToAddress);
//            var smptConfig = email.SmtpConfig;

//            var actualEmailSent = email.SendEmail(mailMessage, smptConfig);

//            AssertAreEqual(expectedEmailSent, actualEmailSent, "Email Sent Flag");
//        }
//    }
//}