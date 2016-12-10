using JoycePrint.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoycePrint.Domain.Tests
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
            var DocketBookModel = new DocketBook();

            AssertAreEqual(null, DocketBookModel.Type, "Docket Book Type");
            AssertAreEqual(null, DocketBookModel.Size, "Docket Book Size");
            AssertAreEqual(0, DocketBookModel.Quantity, "Docket Book Quantity");
        }

        /// <summary>
        /// Tests the getting and setting of property values for the model
        /// </summary>
        [TestMethod]
        public void DocketBookPropertiesTest()
        {
            var DocketBookModel = new DocketBook();

            DocketBookModel.Type = Enums.DocketBookType.Duplicate;
            AssertAreEqual(Enums.DocketBookType.Duplicate, DocketBookModel.Type, "Type");

            DocketBookModel.Type = Enums.DocketBookType.Triplicate;
            AssertAreEqual(Enums.DocketBookType.Triplicate, DocketBookModel.Type, "Type");

            DocketBookModel.Type = Enums.DocketBookType.Quad;
            AssertAreEqual(Enums.DocketBookType.Quad, DocketBookModel.Type, "Type");

            DocketBookModel.Size = Enums.DocketBookSize.A4;
            AssertAreEqual(Enums.DocketBookSize.A4, DocketBookModel.Size, "Size");

            DocketBookModel.Size = Enums.DocketBookSize.A5;
            AssertAreEqual(Enums.DocketBookSize.A5, DocketBookModel.Size, "Size");

            DocketBookModel.Size = Enums.DocketBookSize.A6;
            AssertAreEqual(Enums.DocketBookSize.A6, DocketBookModel.Size, "Size");

            DocketBookModel.Size = Enums.DocketBookSize.DL;
            AssertAreEqual(Enums.DocketBookSize.DL, DocketBookModel.Size, "Size");

            var QuantityTestValue = 100;
            DocketBookModel.Quantity = QuantityTestValue;
            AssertAreEqual(QuantityTestValue, DocketBookModel.Quantity, "Size");
        }
    }
}
