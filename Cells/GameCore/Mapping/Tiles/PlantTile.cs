using System;

namespace Cells.GameCore.Mapping.Tiles
{
    public class PlantTile: MapTile
    {
        private short _plantReserve = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">x coordinate of the element</param>
        /// <param name="y">y coordinate of the element</param>
        public PlantTile(short x, short y)
            : base (x, y)
        {

        }
    }
}
