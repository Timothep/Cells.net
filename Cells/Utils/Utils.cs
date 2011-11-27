using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.Brain;
using Cells.GameCore;
using Cells.GameCore.Cells;
using Cells.Interfaces;
using Cells.Properties;
using Ninject;
using Ninject.Modules;

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

    public class GlobalWorldModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorld>().To<World>().InSingletonScope();
        }
    }

    public class NinjectGlobalKernel
    {
        public static IKernel GlobalKernel = new StandardKernel(new GlobalWorldModule());
    }
}
