using Cells.Utils;

namespace Cells.GameCore.Mapping.Tiles
{
    public class MapTile
    {
        private Coordinates _position;

        /// <summary>
        /// Sets the position of the current element
        /// </summary>
        /// <param name="x">x coordinate of the element</param>
        /// <param name="y">y coordinate of the element</param>
        /// <returns></returns>
        public void SetPositions(short x, short y)
        {
            _position = new Coordinates(x, y);
        }

        /// <summary>
        /// Retrieves the position of the current element
        /// </summary>
        /// <returns></returns>
        public Coordinates GetPosition()
        {
            return _position;
        }
    }
}
