using Cells.Utils;

namespace Cells.GameCore.Mapping.Tiles
{
    public class MapTile
    {
        private readonly Coordinates _position;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">x coordinate of the element</param>
        /// <param name="y">y coordinate of the element</param>
        public MapTile(short x, short y)
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
