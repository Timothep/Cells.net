using Cells.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class EngineTest
    {
        [TestMethod]
        public void TestGetCellsToPaintNotNull()
        {
            GameController testEngine = new GameController();
            testEngine.StartGame();
            Assert.IsNotNull(testEngine.GetUpdatedElements());
        }
    }
}
