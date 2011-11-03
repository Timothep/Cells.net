using Cells.Utils;

namespace Cells.GameCore.Mapping.Tiles
{
    public class MapTile
    {
        private readonly Coordinates _position;
        //private Boolean _occupancy = false;
        //private Int32 _plantReserve = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="X">x coordinate of the element</param>
        /// <param name="Y">y coordinate of the element</param>
        public MapTile(int X, int Y)
        {
            this._position = new Coordinates(X, Y);
        }

        /// <summary>
        /// Retrieves the position of the current element
        /// </summary>
        /// <returns></returns>
        public Coordinates GetPosition()
        {
            return this._position;
        }

        ///// <summary>
        ///// Marks the coordinates as busy
        ///// </summary>
        //public void SetOccupied()
        //{
        //    this._occupancy = true;
        //}

        ///// <summary>
        ///// Marks the coordinates as free
        ///// </summary>
        //public void SetFree()
        //{
        //    this._occupancy = false;
        //}

        ///// <summary>
        ///// Tests if a coordinate is busy
        ///// </summary>
        ///// <returns>True if the cell is occupied, false otherwise</returns>
        //public bool IsOccupied()
        //{
        //    return this._occupancy ? true : false;
        //}
    }
}
