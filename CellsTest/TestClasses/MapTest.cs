using Cells.GameCore.Mapping;
using Cells.GameCore.Mapping.Tiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cells.Utils;
using System.Collections.Generic;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void TestGetMapExtractNotNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Assert.IsNotNull(testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 5, 5));
        }

        [TestMethod]
        public void TestGetMapExtractColumnsNumberOk()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 3, 3);
            Assert.IsTrue(extract.GetMapWidth() == 3);
        }

        [TestMethod]
        public void TestNullParameterReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(null, 3, 3);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestEvenParameterWidthReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 2, 3);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestEvenParameterHeightReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 3, 2);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestNegativeParameterWidthReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(1, 1), -1, 3);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestNegativeParameterHeightReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 3, -1);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestTooBigParameterWidthReturnsNull()
        {
            Map testMap = new Map(3, 3);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 5, 5);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestTooBigParameterHeightReturnsNull()
        {
            Map testMap = new Map(3, 3);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 5, 5);
            Assert.IsNull(extract);
        }
        
        [TestMethod]
        public void TestGetMapExtractHasOneItem()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Assert.IsTrue(testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 3, 3).GetMapWidth() > 0);
        }

        [TestMethod]
        public void TestGetMapExtractRowsNumberOk()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(1, 1), 3, 3);
            Assert.IsTrue(extract.GetMapHeight() >= 1);
        }

        [TestMethod]
        public void TestGetMapExtractOutterBound()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Map extract = testMap.GetSubsetOfThisMap(new Coordinates(9, 9), 3, 3);
            Assert.IsTrue(extract.GetMapWidth() == 2);
        }

    }
}
