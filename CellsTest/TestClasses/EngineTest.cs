using Cells.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class EngineTest
    {
        [TestMethod]
        public void TestGetCellsToPaintNotNull()
        {
            GameController testEngine = new GameController();
            testEngine.StartGame(String.Empty);
            Assert.IsNotNull(testEngine.GetPixelsToPaint());
        }
    }
}
