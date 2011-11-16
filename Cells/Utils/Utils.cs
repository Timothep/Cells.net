using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.Properties;

namespace Cells.Utils
{
    static public class RandomGenerator
    {
        static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        static public Int32 GetRandomInteger(Int32 max)
        {
            return Rand.Next(max);
        }

        static public Int16 GetRandomInt16(Int16 max)
        {
            return (Int16)Rand.Next(max);
        }
    }

    static public class Helper
    {
        static public Boolean CoordinatesAreValid(Int16 coordX, Int16 coordY)
        {
            if (coordX < 0 || coordX > Settings.Default.WorldWidth
             || coordY < 0 || coordY > Settings.Default.WorldHeight)
                return false;

            return true;
        }
    }
}
