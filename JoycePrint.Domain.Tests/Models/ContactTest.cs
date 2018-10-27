using JoycePrint.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests.Models
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
            var contactModel = new Contact();

            AssertAreEqual(null, contactModel.Company, "Contact Company");
            AssertAreEqual(null, contactModel.Name, "Contact Name");
            AssertAreEqual(null, contactModel.Phone, "Contact Company");
            AssertAreEqual(null, contactModel.Email, "Contact Email");
        }

        /// <summary>
        /// Tests the getting and setting of property values for the model
        /// </summary>
        [TestMethod]
        public void ContactPropertiesTest()
        {
            var contactModel = new Contact();

            const string companyTestValue = "Company";
            contactModel.Company = companyTestValue;
            AssertAreEqual(companyTestValue, contactModel.Company, "Contact Company");

            const string nameTestValue = "Firstname Lastname";
            contactModel.Name = nameTestValue;
            AssertAreEqual(nameTestValue, contactModel.Name, "Contact Name");

            const string phoneTestValue = "3033033003";
            contactModel.Phone = phoneTestValue;
            AssertAreEqual(phoneTestValue, contactModel.Phone, "Contact Phone");

            const string emailTestValue = "testemail@host.com";
            contactModel.Email = emailTestValue;
            AssertAreEqual(emailTestValue, contactModel.Email, "Contact Email");
        }
    }
}