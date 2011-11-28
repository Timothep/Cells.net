using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cells.Utils;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void TestValidCoordinates()
        {
            Assert.IsTrue(Helper.CoordinatesAreValid(0,0));
        }

        [TestMethod]
        public void TestNegativeXCoordinates()
        {
            Assert.IsFalse(Helper.CoordinatesAreValid(-1, 0));
        }

        [TestMethod]
        public void TestNegativeYCoordinates()
        {
            Assert.IsFalse(Helper.CoordinatesAreValid(1, -1));
        }

        [TestMethod]
        public void TestBorderOutYCoordinates()
        {
            Assert.IsFalse(Helper.CoordinatesAreValid(1, 100));
        }

        [TestMethod]
        public void TestBorderOutXCoordinates()
        {
            Assert.IsFalse(Helper.CoordinatesAreValid(100, 1));
        }
    }
}
