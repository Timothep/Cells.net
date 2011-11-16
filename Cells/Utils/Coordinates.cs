using System;
using Cells.Properties;
namespace Cells.Utils
{
    /// <summary>
    /// Class representing a vector of coordinates X and Y
    /// </summary>
    public class Coordinates
    {
        public Int16 X { get; set; }
        public Int16 Y { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Coordinates(Int16 inX, Int16 inY)
        {
            X = inX;
            Y = inY;
        }

        /// <summary>
        /// Clones the current position
        /// </summary>
        /// <returns>A new Coordinates object cloned</returns>
        public Coordinates Clone()
        {
            return new Coordinates(X, Y);
        }
    }
}
