using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cells.Utils
{
    static public class RandomGenerator
    {
        static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        static public Int32 GetRandomInteger(Int32 max)
        {
            return Rand.Next(max);
        }
    }
}
