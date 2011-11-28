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
    /// <summary>
    /// Random generator
    /// </summary>
    static public class RandomGenerator
    {
        static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        static public Int32 GetRandomInt32(Int32 max)
        {
            return Rand.Next(max);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        static public Int16 GetRandomInt16(Int16 max)
        {
            return (Int16)Rand.Next(max);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static public class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordX"></param>
        /// <param name="coordY"></param>
        /// <returns></returns>
        static public Boolean CoordinatesAreValid(Int16 coordX, Int16 coordY)
        {
            if (coordX < 0 || coordX >= Settings.Default.WorldWidth
             || coordY < 0 || coordY >= Settings.Default.WorldHeight)
                return false;

            return true;
        }
    }

    /// <summary>
    /// This class holds a reference to a global Ninject Module
    /// It is used to hold a singleton reference to the "world" object
    /// </summary>
    public class GlobalWorldModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorld>().To<World>().InSingletonScope();
        }
    }

    /// <summary>
    /// This class holds a reference to a global Ninject kernel
    /// It is used to hold a singleton reference to the "world" object
    /// </summary>
    public class NinjectGlobalKernel
    {
        public static IKernel GlobalKernel = new StandardKernel(new GlobalWorldModule());
    }
}
