namespace Cells.Utils
{
    /// <summary>
    /// Class representing a vector of coordinates X and Y
    /// </summary>
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Coordinates(int inX, int inY)
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
