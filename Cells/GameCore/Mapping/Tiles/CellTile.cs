using System;

namespace Cells.GameCore.Mapping.Tiles
{
    public class CellTile: MapTile
    {
        private Boolean _occupancy = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public CellTile()
        {

        }

        /// <summary>
        /// Marks the coordinates as busy
        /// </summary>
        public void SetOccupied()
        {
            _occupancy = true;
        }

        /// <summary>
        /// Marks the coordinates as free
        /// </summary>
        public void SetFree()
        {
            _occupancy = false;
        }

        /// <summary>
        /// Tests if a coordinate is busy
        /// </summary>
        /// <returns>True if the cell is occupied, false otherwise</returns>
        public bool IsOccupied()
        {
            return _occupancy;
        }
    }
}
