using Cells.GameCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class EngineTest
    {
        [TestMethod]
        public void TestGetCellsToPaintNotNull()
        {
            GameCoreEngine testEngine = new GameCoreEngine();
            testEngine.StartGame();
            Assert.IsNotNull(testEngine.GetUpdatedElements());
        }
    }
}
