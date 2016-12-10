using JoycePrint.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests
{
    [TestClass]
    public class ContactTest : BaseTest
    {
        /// <summary>
        /// Verify the contact model is created with the correct properties and values
        /// </summary>
        [TestMethod]
        public void CreateContactModelTest()
        {
            var ContactModel = new Contact();

            AssertAreEqual(null, ContactModel.Company, "Contact Company");
            AssertAreEqual(null, ContactModel.Position, "Contact Position");
            AssertAreEqual(null, ContactModel.Name, "Contact Name");
            AssertAreEqual(null, ContactModel.Phone, "Contact Company");
            AssertAreEqual(null, ContactModel.Email, "Contact Email");
        }

        /// <summary>
        /// Tests the getting and setting of property values for the model
        /// </summary>
        [TestMethod]
        public void ContactPropertiesTest()
        {
            var ContactModel = new Contact();

            var CompanyTestValue = "Company";
            ContactModel.Company = CompanyTestValue;
            AssertAreEqual(CompanyTestValue, ContactModel.Company, "Contact Company");

            var PositionTestValue = "Position";
            ContactModel.Position = PositionTestValue;
            AssertAreEqual(PositionTestValue, ContactModel.Position, "Contact Position");

            var NameTestValue = "Firstname Lastname";
            ContactModel.Name = NameTestValue;
            AssertAreEqual(NameTestValue, ContactModel.Name, "Contact Name");

            var PhoneTestValue = "3033033003";
            ContactModel.Phone = PhoneTestValue;
            AssertAreEqual(PhoneTestValue, ContactModel.Phone, "Contact Phone");

            var EmailTestValue = "testemail@host.com";
            ContactModel.Email = EmailTestValue;
            AssertAreEqual(EmailTestValue, ContactModel.Email, "Contact Email");
        }
    }
}
