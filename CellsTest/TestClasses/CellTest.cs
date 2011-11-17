using System.Drawing;
using Cells.GameCore;
using Cells.GameCore.Cells;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cells.Utils;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class CellTest
    {
        [TestMethod]
        public void TestCellPositionNotNull()
        {
            Cell testCell = new Cell(0, 0, 5, null, Color.Black);
            Assert.IsNotNull(testCell.Position);
        }

        [TestMethod]
        public void TestCellPositionXY()
        {
            const short x = 30;
            const short y = 30;

            Cell testCell = new Cell(x, y, 5, null, Color.Black);
            Assert.IsTrue(testCell.Position.X == x && testCell.Position.Y == y);
        }

        [TestMethod]
        public void TestThinkReturnAction()
        {
            Cell testCell = new Cell(0, 0, 5, new World(), Color.Black);
            Assert.IsNotNull(testCell.Think());
        }

        [TestMethod]
        public void TestSenseNotNull()
        {
            World testWorld = new World();
            Cell testCell = new Cell(0, 0, 5, testWorld, Color.Black);
            Assert.IsNotNull(testCell.Sense());
        }

        [TestMethod]
        public void TestGetDirectionNotNull()
        {
            Cell testCell = new Cell(0, 0, 10, new World(), Color.Black);
            Assert.IsNotNull(testCell.GetRelativePosition(new Coordinates(0,1)));
        }

        [TestMethod]
        public void TestGetDirectionDown()
        {
            Cell testCell = new Cell(2, 2, 10, new World(), Color.Black);
            Assert.IsTrue(testCell.GetRelativePosition(new Coordinates(2, 3)) == CellAction.MOVEDOWN);
        }

        [TestMethod]
        public void TestGetDirectionUp()
        {
            Cell testCell = new Cell(2, 2, 10, new World(), Color.Black);
            Assert.IsTrue(testCell.GetRelativePosition(new Coordinates(2, 0)) == CellAction.MOVEUP);
        }

        [TestMethod]
        public void TestGetDirectionLeft()
        {
            Cell testCell = new Cell(2, 2, 10, new World(), Color.Black);
            Assert.IsTrue(testCell.GetRelativePosition(new Coordinates(0, 2)) == CellAction.MOVELEFT);
        }

        [TestMethod]
        public void TestGetDirectionRight()
        {
            Cell testCell = new Cell(2, 2, 10, new World(), Color.Black);
            Assert.IsTrue(testCell.GetRelativePosition(new Coordinates(3, 2)) == CellAction.MOVERIGHT);
        }

        [TestMethod]
        public void TestGetDirectionUpRight()
        {
            Cell testCell = new Cell(2, 2, 10, new World(), Color.Black);
            CellAction action = testCell.GetRelativePosition(new Coordinates(3, 1));
            Assert.IsTrue(action == CellAction.MOVEUP || action == CellAction.MOVERIGHT);
        }

        [TestMethod]
        public void TestGetDirectionUpLeft()
        {
            Cell testCell = new Cell(2, 2, 10, new World(), Color.Black);
            CellAction action = testCell.GetRelativePosition(new Coordinates(1, 1));
            Assert.IsTrue(action == CellAction.MOVEUP || action == CellAction.MOVELEFT);
        }

        [TestMethod]
        public void TestGetDirectionDownLeft()
        {
            Cell testCell = new Cell(2, 2, 10, new World(), Color.Black);
            CellAction action = testCell.GetRelativePosition(new Coordinates(1, 3));
            Assert.IsTrue(action == CellAction.MOVEDOWN|| action == CellAction.MOVELEFT);
        }

        [TestMethod]
        public void TestGetDirectionDownRight()
        {
            Cell testCell = new Cell(2, 2, 10, new World(), Color.Black);
            CellAction action = testCell.GetRelativePosition(new Coordinates(3, 3));
            Assert.IsTrue(action == CellAction.MOVEDOWN || action == CellAction.MOVERIGHT);
        }
    }
}
