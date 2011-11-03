namespace Cells.GameCore.Mapping.Tiles
{
    public class RessourceTile: MapTile
    {
        private int _ressources = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">x coordinate of the element</param>
        /// <param name="y">y coordinate of the element</param>
        public RessourceTile(int x, int y)
            : base (x, y)
        {

        }
    }
}
