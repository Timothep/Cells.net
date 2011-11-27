using System;
using Cells.Properties;
using Cells.Interfaces;

namespace Cells.Utils
{
    /// <summary>
    /// Class representing a vector of coordinates X and Y
    /// </summary>
    public class Coordinates : ICoordinates
    {
        public Int16 X { get; set; }
        public Int16 Y { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Coordinates()
        {
        }

        /// <summary>
        /// Sets the coordinates
        /// </summary>
        /// <param name="inX">The X position</param>
        /// <param name="inY">The Y position</param>
        public void SetCoordinates(Int16 inX, Int16 inY)
        {
            X = inX;
            Y = inY;
        }

        /// <summary>
        /// Clones the current position
        /// </summary>
        /// <returns>A new Coordinates object cloned</returns>
        public ICoordinates Clone()
        {
            ICoordinates coord = new Coordinates();
            coord.SetCoordinates(X, Y);
            return coord;
        }
    }
}
