using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.Interfaces;

namespace Cells.Model
{
    class OffsetVector : IOffsetVector
    {
        public Int16 X { get; set; }
        public Int16 Y { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OffsetVector()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OffsetVector(ICoordinates coordinatesFrom, ICoordinates coordinatesTo)
        {
            this.X = Convert.ToInt16(coordinatesFrom.X - coordinatesTo.X);
            this.Y = Convert.ToInt16(coordinatesFrom.Y - coordinatesTo.Y);
        } 

        /// <summary>
        /// Sets the coordinates
        /// </summary>
        /// <param name="inX">The X position</param>
        /// <param name="inY">The Y position</param>
        public void SetOffset(Int16 inX, Int16 inY)
        {
            X = inX;
            Y = inY;
        }

        /// <summary>
        /// Clones the current position
        /// </summary>
        /// <returns>A new Coordinates object cloned</returns>
        public IOffsetVector Clone()
        {
            IOffsetVector vector = new OffsetVector();
            vector.SetOffset(X, Y);
            return vector;
        }

    }
}
