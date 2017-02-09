using System;
using System.Text;
using JoycePrint.Domain.Mail;
using JoycePrint.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests.Models
{
    [TestClass]
    public class QuoteRequestTest : BaseTest
    {
        /// <summary>
        /// Verify the quote request model is created with the correct properties and values
        /// </summary>
        [TestMethod]
        public void CreateContactModelTest()
        {
            var quoteRequestModel = new QuoteRequest();

            AssertAreEqual(null, quoteRequestModel.Contact.Company, "Contact Company");
            AssertAreEqual(null, quoteRequestModel.Contact.Position, "Contact Position");
            AssertAreEqual(null, quoteRequestModel.Contact.Name, "Contact Name");
            AssertAreEqual(null, quoteRequestModel.Contact.Phone, "Contact Company");
            AssertAreEqual(null, quoteRequestModel.Contact.Email, "Contact Email");

            AssertAreEqual(null, quoteRequestModel.DocketBook.Type, "Docket Book Type");
            AssertAreEqual(null, quoteRequestModel.DocketBook.Size, "Docket Book Size");
            AssertAreEqual(0, quoteRequestModel.DocketBook.Quantity, "Docket Book Quantity");

            AssertAreEqual(null, quoteRequestModel.Enquiry.Message, "Message");
        }

        /// <summary>
        /// Tests the getting and setting of property values for the model
        /// </summary>
        [TestMethod]
        public void QuotePropertiesTest()
        {
            var quoteRequestModel = new QuoteRequest();

            const string messageTestValue = "Message";
            quoteRequestModel.Enquiry.Message = messageTestValue;
            AssertAreEqual(messageTestValue, quoteRequestModel.Enquiry.Message, "Message");
        }

        /// <summary>
        /// Tests the GetSubjectLine method with a good name
        /// </summary>
        [TestMethod]
        public void GetSubjectLineGoodTest()
        {
            var quoteRequestModel = new QuoteRequest();

            const string nameTestValue = "First Last";
            var expected = $"Docket Book Quote : {nameTestValue}";

            quoteRequestModel.Contact.Name = nameTestValue;

            var actual = quoteRequestModel.GetSubjectLine();

            AssertAreEqual(expected, actual, "Subject Line");
        }

        /// <summary>
        /// Tests the GetSubjectLine method with an empty name
        /// </summary>
        [TestMethod]
        public void GetSubjectLineEmptyContactNameTest()
        {
            var quoteRequestModel = new QuoteRequest();

            var nameTestValue = string.Empty;
            var expected = $"Docket Book Quote : {nameTestValue}";

            quoteRequestModel.Contact.Name = nameTestValue;

            var actual = quoteRequestModel.GetSubjectLine();

            AssertAreEqual(expected, actual, "Subject Line");
        }

        /// <summary>
        /// Tests the GetSubjectLine method with a null name
        /// </summary>
        [TestMethod]
        public void GetSubjectLineNullContactNameTest()
        {
            var quoteRequestModel = new QuoteRequest();

            string nameTestValue = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var expected = $"Docket Book Quote : {nameTestValue}";

            // ReSharper disable once ExpressionIsAlwaysNull
            quoteRequestModel.Contact.Name = nameTestValue;

            var actual = quoteRequestModel.GetSubjectLine();

            AssertAreEqual(expected, actual, "Subject Line");
        }

        /// <summary>
        /// Tests the get message attachments function
        /// </summary>
        [TestMethod]
        public void GetMessageAttachementsTest()
        {
            var quoteRequestModel = new QuoteRequest();
            const string expected = "Attachments are not currently unavailable on the site";

            try
            {
                quoteRequestModel.GetMessageAttachments();
            }
            catch (NotImplementedException ex)
            {
                AssertAreEqual(expected, ex.Message, "Attachments");
            }
        }

        /// <summary>
        /// Creates an empty quote request and verifies the message body is created correctly
        /// </summary>
        [TestMethod]
        public void GetMessageBody_EmptyModelTest()
        {
            var quoteRequestModel = GenerateEmptyQuoteRequestModel();

            var expectedMessageBody = GenerateExpectedMessageBodyForEmptyQuoteRequestModel();

            var actualMessageBody = quoteRequestModel.GetMessageBody();

            AssertAreEqual(expectedMessageBody, actualMessageBody, "email message");
        }

        /// <summary>
        /// Creates a real quote request and verifies the message body is created correctly
        /// </summary>
        [TestMethod]
        public void GetMessageBody_RealModelTest()
        {
            _realQuoteDocketType = Enums.DocketBookType.Duplicate;
            _realQuoteDocketSize = Enums.DocketBookSize.A4;

            var quoteRequestModel = GenerateRealQuoteRequestModel();

            var expectedMessageBody = GenerateExpectedMessageBodyForRealQuoteRequestModel();

            var actualMessageBody = quoteRequestModel.GetMessageBody();

            AssertAreEqual(expectedMessageBody, actualMessageBody, "email message");
        }

        /// <summary>
        /// Creates a real quote request and verifies the message body is created correctly, each enum on the DocketType enum collection will be used
        /// </summary>
        [TestMethod]
        public void GetMessageBody_DocketTypeTest()
        {
            _realQuoteDocketSize = Enums.DocketBookSize.A4;

            foreach (var docketType in Enum.GetValues(typeof(Enums.DocketBookType)))
            {
                _realQuoteDocketType = (Enums.DocketBookType)Enum.Parse(typeof(Enums.DocketBookType), docketType.ToString());

                var quoteRequestModel = GenerateRealQuoteRequestModel();

                var expectedMessageBody = GenerateExpectedMessageBodyForRealQuoteRequestModel();

                var actualMessageBody = quoteRequestModel.GetMessageBody();

                AssertAreEqual(expectedMessageBody, actualMessageBody, "email message");
            }
        }

        /// <summary>
        /// Creates a real quote request and verifies the message body is created correctly, each enum on the DocketSize enum collection will be used
        /// </summary>
        [TestMethod]
        public void GetMessageBody_DocketSizeTest()
        {
            _realQuoteDocketType = Enums.DocketBookType.Duplicate;

            foreach (var docketSize in Enum.GetValues(typeof(Enums.DocketBookSize)))
            {
                _realQuoteDocketSize = (Enums.DocketBookSize)Enum.Parse(typeof(Enums.DocketBookSize), docketSize.ToString());

                var quoteRequestModel = GenerateRealQuoteRequestModel();

                var expectedMessageBody = GenerateExpectedMessageBodyForRealQuoteRequestModel();

                var actualMessageBody = quoteRequestModel.GetMessageBody();

                AssertAreEqual(expectedMessageBody, actualMessageBody, "email message");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// TODO: This test is currently breaking because the configuration element is not getting pulled correctly
        [TestMethod]
        public void ConvertModelToEmailTest()
        {
            IEmail email = new Email();

            _realQuoteDocketType = Enums.DocketBookType.Duplicate;
            _realQuoteDocketSize = Enums.DocketBookSize.A4;

            var quoteRequestModel = GenerateRealQuoteRequestModel();

            var mailMessage = quoteRequestModel.ConvertModelToEmail(email);
        }

        #region Test Helper Functions

        private static string RealQuoteCompany => "Company";

        private static string RealQuotePosition => "Position";

        private static string RealQuoteName => "Firstname Lastname";

        private static string RealQuotePhone => "3033033003";

        private static string RealQuoteEmail => "email@host.com";

        private static int RealQuoteQuantity => 100;
        
        private static string RealQuoteMessage => "Can I get a quote before the end of the week, there's a real rush on this job.";

        private Enums.DocketBookType _realQuoteDocketType;

        private Enums.DocketBookSize _realQuoteDocketSize;

        private QuoteRequest GenerateRealQuoteRequestModel()
        {
            var quoteRequest = new QuoteRequest
            {
                Contact =
                {
                    Company = RealQuoteCompany,
                    Position = RealQuotePosition,
                    Name = RealQuoteName,
                    Phone = RealQuotePhone,
                    Email = RealQuoteEmail
                },
                DocketBook =
                {
                    Type = _realQuoteDocketType,
                    Size = _realQuoteDocketSize,
                    Quantity = RealQuoteQuantity
                },
                Enquiry =
                {
                  Message = RealQuoteMessage
                } 
            };

            return quoteRequest;
        }

        private string GenerateExpectedMessageBodyForRealQuoteRequestModel()
        {
            var messageBody = new StringBuilder();

            messageBody.Append("<h1>Client Information</h1>");
            messageBody.Append("<dl>");
            messageBody.Append("<dt><strong>Company<strong></dt>");
            messageBody.Append($"<dd>{RealQuoteCompany}</dd>");
            messageBody.Append("<dt><strong>Position</strong></dt>");
            messageBody.Append($"<dd>{RealQuotePosition}</dd>");
            messageBody.Append("<dt><strong>Name</strong></dt>");
            messageBody.Append($"<dd>{RealQuoteName}</dd>");
            messageBody.Append("<dt><strong>Telephone</strong></dt>");
            messageBody.Append($"<dd>{RealQuotePhone}</dd>");
            messageBody.Append("<dt><strong>Email</strong></dt>");
            messageBody.Append($"<dd>{RealQuoteEmail}</dd>");
            messageBody.Append("</dl>");
            messageBody.Append("<h1>Product Information</h1>");
            messageBody.Append("<dl>");
            messageBody.Append("<dt><strong>Docket Type</strong></dt>");
            messageBody.Append($"<dd>{_realQuoteDocketType}</dd>");
            messageBody.Append("<dt><strong>Docket Size</strong></dt>");
            messageBody.Append($"<dd>{_realQuoteDocketSize}</dd>");
            messageBody.Append("<dt><strong>Quantity</strong></dt>");
            messageBody.Append($"<dd>{RealQuoteQuantity}</dd>");
            messageBody.Append("</dl>");
            messageBody.Append($"<div><strong>User message</strong><p>{RealQuoteMessage}</p></div>");

            return messageBody.ToString();
        }
        
        private static QuoteRequest GenerateEmptyQuoteRequestModel()
        {
            var quoteRequest = new QuoteRequest();
            return quoteRequest;
        }
       
        private static string GenerateExpectedMessageBodyForEmptyQuoteRequestModel()
        {
            var messageBody = new StringBuilder();

            messageBody.Append("<h1>Client Information</h1>");
            messageBody.Append("<dl>");
            messageBody.Append("<dt><strong>Company<strong></dt>");
            messageBody.Append($"<dd></dd>");
            messageBody.Append("<dt><strong>Position</strong></dt>");
            messageBody.Append($"<dd></dd>");
            messageBody.Append("<dt><strong>Name</strong></dt>");
            messageBody.Append($"<dd></dd>");
            messageBody.Append("<dt><strong>Telephone</strong></dt>");
            messageBody.Append($"<dd></dd>");
            messageBody.Append("<dt><strong>Email</strong></dt>");
            messageBody.Append($"<dd></dd>");
            messageBody.Append("</dl>");
            messageBody.Append("<h1>Product Information</h1>");
            messageBody.Append("<dl>");
            messageBody.Append("<dt><strong>Docket Type</strong></dt>");
            messageBody.Append($"<dd></dd>");
            messageBody.Append("<dt><strong>Docket Size</strong></dt>");
            messageBody.Append($"<dd></dd>");
            messageBody.Append("<dt><strong>Quantity</strong></dt>");
            messageBody.Append($"<dd>{0}</dd>");
            messageBody.Append("</dl>");
            messageBody.Append($"<div><strong>User message</strong><p></p></div>");

            return messageBody.ToString();
        }

        #endregion
    }
}