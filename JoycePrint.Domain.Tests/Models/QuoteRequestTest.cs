using JoycePrint.Domain.Business;
using JoycePrint.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace JoycePrint.Domain.Tests
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
            var QuoteRequestModel = new QuoteRequest();

            AssertAreEqual(null, QuoteRequestModel.Contact.Company, "Contact Company");
            AssertAreEqual(null, QuoteRequestModel.Contact.Position, "Contact Position");
            AssertAreEqual(null, QuoteRequestModel.Contact.Name, "Contact Name");
            AssertAreEqual(null, QuoteRequestModel.Contact.Phone, "Contact Company");
            AssertAreEqual(null, QuoteRequestModel.Contact.Email, "Contact Email");

            AssertAreEqual(null, QuoteRequestModel.DocketBook.Type, "Docket Book Type");
            AssertAreEqual(null, QuoteRequestModel.DocketBook.Size, "Docket Book Size");
            AssertAreEqual(0, QuoteRequestModel.DocketBook.Quantity, "Docket Book Quantity");

            AssertAreEqual(null, QuoteRequestModel.Message, "Message");
        }

        /// <summary>
        /// Tests the getting and setting of property values for the model
        /// </summary>
        [TestMethod]
        public void QuotePropertiesTest()
        {
            var QuoteRequestModel = new QuoteRequest();

            var MessageTestValue = "Message";
            QuoteRequestModel.Message = MessageTestValue;
            AssertAreEqual(MessageTestValue, QuoteRequestModel.Message, "Message");
        }

        /// <summary>
        /// Tests the GetSubjectLine method with a good name
        /// </summary>
        [TestMethod]
        public void GetSubjectLineGoodTest()
        {
            var QuoteRequestModel = new QuoteRequest();

            var NameTestValue = "First Last";
            var expected = $"Docket Book Quote : {NameTestValue}";

            QuoteRequestModel.Contact.Name = NameTestValue;

            var actual = QuoteRequestModel.GetSubjectLine();

            AssertAreEqual(expected, actual, "Subject Line");
        }

        /// <summary>
        /// Tests the GetSubjectLine method with an empty name
        /// </summary>
        [TestMethod]
        public void GetSubjectLineEmptyContactNameTest()
        {
            var QuoteRequestModel = new QuoteRequest();

            var NameTestValue = string.Empty;
            var expected = $"Docket Book Quote : {NameTestValue}";

            QuoteRequestModel.Contact.Name = NameTestValue;

            var actual = QuoteRequestModel.GetSubjectLine();

            AssertAreEqual(expected, actual, "Subject Line");
        }

        /// <summary>
        /// Tests the GetSubjectLine method with a null name
        /// </summary>
        [TestMethod]
        public void GetSubjectLineNullContactNameTest()
        {
            var QuoteRequestModel = new QuoteRequest();

            string NameTestValue = null;
            var expected = $"Docket Book Quote : {NameTestValue}";

            QuoteRequestModel.Contact.Name = NameTestValue;

            var actual = QuoteRequestModel.GetSubjectLine();

            AssertAreEqual(expected, actual, "Subject Line");
        }

        /// <summary>
        /// Tests the get message attachments function
        /// </summary>
        [TestMethod]
        public void GetMessageAttachementsTest()
        {
            var QuoteRequestModel = new QuoteRequest();
            var expected = "Attachments are not currently unavailable on the site";

            try
            {
                QuoteRequestModel.GetMessageAttachments();
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
            var QuoteRequestModel = GenerateEmptyQuoteRequestModel();

            var expectedMessageBody = GenerateExpectedMessageBodyForEmptyQuoteRequestModel();

            var actualMessageBody = QuoteRequestModel.GetMessageBody();

            AssertAreEqual(expectedMessageBody, actualMessageBody, "email message");
        }

        /// <summary>
        /// Creates a real quote request and verifies the message body is created correctly
        /// </summary>
        [TestMethod]
        public void GetMessageBody_RealModelTest()
        {
            RealQuote_DocketType = Enums.DocketBookType.Duplicate;
            RealQuote_DocketSize = Enums.DocketBookSize.A4;

            var QuoteRequestModel = GenerateRealQuoteRequestModel();

            var expectedMessageBody = GenerateExpectedMessageBodyForRealQuoteRequestModel();

            var actualMessageBody = QuoteRequestModel.GetMessageBody();

            AssertAreEqual(expectedMessageBody, actualMessageBody, "email message");
        }

        /// <summary>
        /// Creates a real quote request and verifies the message body is created correctly, each enum on the DocketType enum collection will be used
        /// </summary>
        [TestMethod]
        public void GetMessageBody_DocketTypeTest()
        {
            RealQuote_DocketSize = Enums.DocketBookSize.A4;

            foreach (var docketType in Enum.GetValues(typeof(Enums.DocketBookType)))
            {
                RealQuote_DocketType = (Enums.DocketBookType)Enum.Parse(typeof(Enums.DocketBookType), docketType.ToString());

                var QuoteRequestModel = GenerateRealQuoteRequestModel();

                var expectedMessageBody = GenerateExpectedMessageBodyForRealQuoteRequestModel();

                var actualMessageBody = QuoteRequestModel.GetMessageBody();

                AssertAreEqual(expectedMessageBody, actualMessageBody, "email message");
            }
        }

        /// <summary>
        /// Creates a real quote request and verifies the message body is created correctly, each enum on the DocketSize enum collection will be used
        /// </summary>
        [TestMethod]
        public void GetMessageBody_DocketSizeTest()
        {
            RealQuote_DocketType = Enums.DocketBookType.Duplicate;

            foreach (var docketSize in Enum.GetValues(typeof(Enums.DocketBookSize)))
            {
                RealQuote_DocketSize = (Enums.DocketBookSize)Enum.Parse(typeof(Enums.DocketBookSize), docketSize.ToString());

                var QuoteRequestModel = GenerateRealQuoteRequestModel();

                var expectedMessageBody = GenerateExpectedMessageBodyForRealQuoteRequestModel();

                var actualMessageBody = QuoteRequestModel.GetMessageBody();

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
            IEmail Email = new Email();

            RealQuote_DocketType = Enums.DocketBookType.Duplicate;
            RealQuote_DocketSize = Enums.DocketBookSize.A4;

            var QuoteRequestModel = GenerateRealQuoteRequestModel();

            var mailMessage = QuoteRequestModel.ConvertModelToEmail(Email);
        }

        #region Test Helper Functions

        private string RealQuote_Company => "Company";

        private string RealQuote_Position => "Position";

        private string RealQuote_Name => "Firstname Lastname";

        private string RealQuote_Phone => "3033033003";

        private string RealQuote_Email => "email@host.com";

        private int RealQuote_Quantity => 100;

        private string RealQuote_Message => "Can I get a quote before the end of the week, there's a real rush on this job.";

        private Enums.DocketBookType RealQuote_DocketType;

        private Enums.DocketBookSize RealQuote_DocketSize;

        private QuoteRequest GenerateRealQuoteRequestModel()
        {
            var quoteRequest = new QuoteRequest();

            quoteRequest.Contact.Company = RealQuote_Company;
            quoteRequest.Contact.Position = RealQuote_Position;
            quoteRequest.Contact.Name = RealQuote_Name;
            quoteRequest.Contact.Phone = RealQuote_Phone;
            quoteRequest.Contact.Email = RealQuote_Email;

            quoteRequest.DocketBook.Type = RealQuote_DocketType;
            quoteRequest.DocketBook.Size = RealQuote_DocketSize;
            quoteRequest.DocketBook.Quantity = RealQuote_Quantity;

            quoteRequest.Message = RealQuote_Message;

            return quoteRequest;
        }

        private string GenerateExpectedMessageBodyForRealQuoteRequestModel()
        {
            var messageBody = new StringBuilder();

            messageBody.Append("<h1>Client Information</h1>");
            messageBody.Append("<dl>");
            messageBody.Append("<dt><strong>Company<strong></dt>");
            messageBody.Append($"<dd>{RealQuote_Company}</dd>");
            messageBody.Append("<dt><strong>Position</strong></dt>");
            messageBody.Append($"<dd>{RealQuote_Position}</dd>");
            messageBody.Append("<dt><strong>Name</strong></dt>");
            messageBody.Append($"<dd>{RealQuote_Name}</dd>");
            messageBody.Append("<dt><strong>Telephone</strong></dt>");
            messageBody.Append($"<dd>{RealQuote_Phone}</dd>");
            messageBody.Append("<dt><strong>Email</strong></dt>");
            messageBody.Append($"<dd>{RealQuote_Email}</dd>");
            messageBody.Append("</dl>");
            messageBody.Append("<h1>Product Information</h1>");
            messageBody.Append("<dl>");
            messageBody.Append("<dt><strong>Docket Type</strong></dt>");
            messageBody.Append($"<dd>{RealQuote_DocketType}</dd>");
            messageBody.Append("<dt><strong>Docket Size</strong></dt>");
            messageBody.Append($"<dd>{RealQuote_DocketSize}</dd>");
            messageBody.Append("<dt><strong>Quantity</strong></dt>");
            messageBody.Append($"<dd>{RealQuote_Quantity}</dd>");
            messageBody.Append("</dl>");
            messageBody.Append($"<div><strong>User message</strong><p>{RealQuote_Message}</p></div>");

            return messageBody.ToString();
        }

        private QuoteRequest GenerateEmptyQuoteRequestModel()
        {
            var quoteRequest = new QuoteRequest();
            return quoteRequest;
        }

        private string GenerateExpectedMessageBodyForEmptyQuoteRequestModel()
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