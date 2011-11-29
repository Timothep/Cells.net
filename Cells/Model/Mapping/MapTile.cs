using System;
using Cells.Interfaces;
using Cells.Utils;
using Ninject;

namespace Cells.Model.Mapping
{
    public class MapTile
    {
        [Inject]
        private ICoordinates _position;

        public ICell CellReference = null;
        public Int16 GrowthRate = 0;
        public Int16 RessourceLevel = 0;
        public Int16 Altitude = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public MapTile()
        {
            _position = new Coordinates();
        }

        /// <summary>
        /// Sets the position of the current element
        /// </summary>
        /// <param name="x">x coordinate of the element</param>
        /// <param name="y">y coordinate of the element</param>
        /// <returns></returns>
        internal void SetPositions(short x, short y)
        {
            _position.X = x;
            _position.Y = y;
        }

        /// <summary>
        /// Retrieves the position of the current element
        /// </summary>
        /// <returns></returns>
        internal ICoordinates GetPosition()
        {
            return _position;
        }

        /// <summary>
        /// Get altitude
        /// </summary>
        /// <returns></returns>
        internal Int16 GetAltitude()
        {
            return this.Altitude;
        }
    }
}
