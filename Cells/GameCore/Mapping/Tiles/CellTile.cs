using System;

namespace Cells.GameCore.Mapping.Tiles
{
    public class CellTile: MapTile
    {
        private Boolean _occupancy = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">x coordinate of the element</param>
        /// <param name="y">y coordinate of the element</param>
        public CellTile(int x, int y)
            : base(x, y)
        {

        }

        /// <summary>
        /// Marks the coordinates as busy
        /// </summary>
        public void SetOccupied()
        {
            this._occupancy = true;
        }

        /// <summary>
        /// Marks the coordinates as free
        /// </summary>
        public void SetFree()
        {
            this._occupancy = false;
        }

        /// <summary>
        /// Tests if a coordinate is busy
        /// </summary>
        /// <returns>True if the cell is occupied, false otherwise</returns>
        public bool IsOccupied()
        {
            return this._occupancy ? true : false;
        }
    }
}
