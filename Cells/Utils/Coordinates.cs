namespace Cells.Utils
{
    public class Coordinates
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Coordinates(int inX, int inY)
        {
            this.X = inX;
            this.Y = inY;
        }

        /// <summary>
        /// Clones the current position
        /// </summary>
        /// <returns>A new Coordinates object cloned</returns>
        public Coordinates Clone()
        {
            return new Coordinates(this.X, this.Y);
        }
    }
}
