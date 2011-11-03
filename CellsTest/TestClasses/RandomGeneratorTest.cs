using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cells.Utils;

namespace CellsTest.TestClasses
{
    [TestClass]
    public class RandomGeneratorTest
    {
        [TestMethod]
        public void TestRandomZeroIsZero()
        {
            Assert.IsTrue(RandomGenerator.GetRandomInteger(0) == 0);
        }

        [TestMethod]
        public void TestRandomIsRandom()
        {
            Assert.AreNotEqual(RandomGenerator.GetRandomInteger(100), RandomGenerator.GetRandomInteger(100));
        }

        [TestMethod]
        public void TestIntegerFrame()
        {
            Int32 dd = RandomGenerator.GetRandomInteger(1);
            Assert.IsTrue(dd <= 1);
        }
    }
}
