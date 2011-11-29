using Cells.Model;
using Cells.Model.Mapping;
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
            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1,1);
            Assert.IsNotNull(testMap.GetSubset(coord, 5, 5));
        }

        [TestMethod]
        public void TestGetMapExtractColumnsNumberOk()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1);
            MapTile[,] extract = testMap.GetSubset(coord, 3, 3);
            Assert.IsTrue(extract.GetLength(0) == 3);
        }

        [TestMethod]
        public void TestNullParameterReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            MapTile[,] extract = testMap.GetSubset(null, 3, 3);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestEvenParameterWidthReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1);
            MapTile[,] extract = testMap.GetSubset(coord, 2, 3);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestEvenParameterHeightReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();

            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1);

            MapTile[,] extract = testMap.GetSubset(coord, 3, 2);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestNegativeParameterWidthReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();

            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1);

            MapTile[,] extract = testMap.GetSubset(coord, -1, 3);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestNegativeParameterHeightReturnsNull()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();

            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1);

            MapTile[,] extract = testMap.GetSubset(coord, 3, -1);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestTooBigParameterWidthReturnsNull()
        {
            Map testMap = new Map(3, 3);
            testMap.InitializeGrid();
            
            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1);

            MapTile[,] extract = testMap.GetSubset(coord, 5, 5);
            Assert.IsNull(extract);
        }

        [TestMethod]
        public void TestTooBigParameterHeightReturnsNull()
        {
            Map testMap = new Map(3, 3);
            testMap.InitializeGrid();

            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1);

            MapTile[,] extract = testMap.GetSubset(coord, 5, 5);
            Assert.IsNull(extract);
        }
        
        [TestMethod]
        public void TestGetMapExtractHasOneItem()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();

            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1); 
            
            Assert.IsTrue(testMap.GetSubset(coord, 3, 3).GetLength(0) > 0);
        }

        [TestMethod]
        public void TestGetMapExtractRowsNumberOk()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();
            
            Coordinates coord = new Coordinates();
            coord.SetCoordinates(1, 1);

            MapTile[,] extract = testMap.GetSubset(coord, 3, 3);
            Assert.IsTrue(extract.GetLength(1) >= 1);
        }

        [TestMethod]
        public void TestGetMapExtractOutterBound()
        {
            Map testMap = new Map(10, 10);
            testMap.InitializeGrid();

            Coordinates coord = new Coordinates();
            coord.SetCoordinates(9, 9);

            MapTile[,] extract = testMap.GetSubset(coord, 3, 3);
            Assert.IsTrue(extract.GetLength(1) == 2);
        }

    }
}
