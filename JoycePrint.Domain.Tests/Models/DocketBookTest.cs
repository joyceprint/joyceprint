using JoycePrint.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests.Models
{
    [TestClass]
    public class DocketBookTest : BaseTest
    {
        /// <summary>
        /// Verify the docket book model is created with the correct properties and values
        /// </summary>
        [TestMethod]
        public void CreateDocketBookModelTest()
        {
            var docketBookModel = new DocketBook();

            AssertAreEqual(null, docketBookModel.Type, "Docket Book Type");
            AssertAreEqual(null, docketBookModel.Size, "Docket Book Size");
            AssertAreEqual(0, docketBookModel.Quantity, "Docket Book Quantity");
        }

        /// <summary>
        /// Tests the getting and setting of property values for the model
        /// </summary>
        [TestMethod]
        public void DocketBookPropertiesTest()
        {
            var docketBookModel = new DocketBook {Type = Enums.DocketBookType.Duplicate};
            AssertAreEqual(Enums.DocketBookType.Duplicate, docketBookModel.Type, "Type");

            docketBookModel.Type = Enums.DocketBookType.Triplicate;
            AssertAreEqual(Enums.DocketBookType.Triplicate, docketBookModel.Type, "Type");

            docketBookModel.Type = Enums.DocketBookType.Quad;
            AssertAreEqual(Enums.DocketBookType.Quad, docketBookModel.Type, "Type");

            docketBookModel.Size = Enums.DocketBookSize.A4;
            AssertAreEqual(Enums.DocketBookSize.A4, docketBookModel.Size, "Size");

            docketBookModel.Size = Enums.DocketBookSize.A5;
            AssertAreEqual(Enums.DocketBookSize.A5, docketBookModel.Size, "Size");

            docketBookModel.Size = Enums.DocketBookSize.A6;
            AssertAreEqual(Enums.DocketBookSize.A6, docketBookModel.Size, "Size");

            docketBookModel.Size = Enums.DocketBookSize.DL;
            AssertAreEqual(Enums.DocketBookSize.DL, docketBookModel.Size, "Size");

            const int quantityTestValue = 100;
            docketBookModel.Quantity = quantityTestValue;
            AssertAreEqual(quantityTestValue, docketBookModel.Quantity, "Size");
        }
    }
}
