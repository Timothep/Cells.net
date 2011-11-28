using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;
using Cells.Interfaces;
using Cells.Model.Mapping;
using Cells.Properties;

namespace Cells.GameCore.Mapping
{
    /// <summary>
    /// Class representing what the cell can "see"
    /// </summary>
    public class SurroundingView : ISurroundingView
    {
        private ICoordinates _centerOfView;
        
        // The view is a square centered on the cell
        private short _ViewSizeX;
        private short _ViewSizeY;

        public Map _view;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="coordinates"></param>
        public SurroundingView(ICoordinates coordinates, MapTile[,] view)
        {
            // Set the center coordinate
            _centerOfView = coordinates;
            _ViewSizeX = Convert.ToInt16(view.GetUpperBound(0));
            _ViewSizeY = Convert.ToInt16(view.GetUpperBound(1));
            _view = new Map(_ViewSizeX, _ViewSizeY);
            _view.InitializeGrid(view);
        }

        /// <summary>
        /// Function returning a list of all the cells present on the view
        /// </summary>
        /// <returns>An IList of ICell</returns>
        public IList<ICell> GetAllCells()
        {
            IList<ICell> newList = new List<ICell>();

            for (int i = 0; i < _ViewSizeX - 1; i++)
                for (int j = 0; j < _ViewSizeY - 1; j++)
                    if (_view.Grid[i,j].CellReference != null)
                        newList.Add(_view.Grid[i, j].CellReference);

            return newList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Int16 GetWidth()
        {
            return this._view.GetMapWidth();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Int16 GetHeight()
        {
            return this._view.GetMapHeight();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //internal ICoordinates GetClosestRessourcePool()
        //{
        //    IList<ICoordinates> ressources = this._view.GetRessourcesList();
        //    ICoordinates closestRessource = null;
        //    Int16? minDistance = null;
        //    foreach(ICoordinates spot in ressources)
        //    {
        //        if (closestRessource == null)
        //        {
        //            closestRessource = spot;
        //            minDistance = this._centerOfView.DistanceTo(spot);
        //        }
        //        if (spot.DistanceTo(closestRessource) < minDistance)
        //        {
        //            closestRessource = spot;
        //            minDistance = this._centerOfView.DistanceTo(spot);
        //        }
        //    }
            
        //    return null;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal ICoordinates GetClosestRessourcePool()
        {
            ICoordinates theOne = null;
            Int16? minDistanceSofar = (Int16)this._view.Grid.GetLength(0);

            for (int i = 0; i < this._view.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < this._view.Grid.GetLength(1); j++)
                {
                    if (this._view.Grid[i, j].RessourceLevel > 0)
                    {
                        var coord = new Coordinates();
                        coord.SetCoordinates((Int16)i, (Int16)j);
                        Int16? distTo = this._centerOfView.DistanceTo(coord);
                        if (distTo < minDistanceSofar)
                        {
                            minDistanceSofar = distTo;
                            theOne = coord;
                        }
                    }
                }
            }

            return theOne;
        }
    }
}
