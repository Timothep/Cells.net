using System;
using Cells.Interfaces;

namespace Cells.Model
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
        /// Constructor
        /// </summary>
        public Coordinates(Int16 inX, Int16 inY)
        {
            this.SetCoordinates(inX, inY);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public Int16? DistanceTo(ICoordinates position)
        {
            if (position == null)
                return null;

            return (Int16)(Math.Abs((UInt16)(this.X - position.X)) +
                   Math.Abs((UInt16)(this.Y - position.Y)));
        }

        public void Normalize(Int16 maxX, Int16 maxY)
        {
            if (this.X > maxX)
                this.X = 0;
            if (this.X < 0)
                this.X = maxX;

            if (this.Y > maxY)
                this.Y = 0;
            if (this.Y < 0)
                this.Y = maxY;
        }
    }
}
