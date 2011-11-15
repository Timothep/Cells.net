using System;
using Cells.GameCore.Cells;
using Cells.Interfaces;
using Cells.Utils;

namespace Cells.GameCore.Mapping.Tiles
{
    public class MapTile: ICellTile, IRessourceTile
    {
        private Coordinates _position = null;
        public Cell CellReference = null;
        public Int16 GrowthRate = 0;
        public Int16 RessourceLevel = 0;
        public Int16 Height = 0;

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
