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
using Cells.Model;

namespace Cells.GameCore.Mapping
{
    /// <summary>
    /// Class representing what the cell can "see"
    /// </summary>
    public class SurroundingView : ISurroundingView
    {
        public ICoordinates CellPositionInWorld;
        public ICoordinates CellPositionInView;
        
        // The view is a square centered on the cell
        private readonly short viewSizeX;
        private readonly short viewSizeY;

        public Map View;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="coordinates"></param>
        public SurroundingView(ICoordinates coordinates, MapTile[,] view)
        {
            // Set the center coordinate
            CellPositionInWorld = coordinates;
            
            viewSizeX = Convert.ToInt16(view.GetUpperBound(0));
            viewSizeY = Convert.ToInt16(view.GetUpperBound(1));

            CellPositionInView = new Coordinates((Int16)(viewSizeX / 2), (Int16)(viewSizeY / 2));

            View = new Map(viewSizeX, viewSizeY);
            View.InitializeGrid(view);
        }

        /// <summary>
        /// Function returning a list of all the cells present on the view
        /// </summary>
        /// <returns>An IList of ICell</returns>
        public IList<ICell> GetAllCells()
        {
            IList<ICell> newList = new List<ICell>();

            for (int i = 0; i < viewSizeX - 1; i++)
                for (int j = 0; j < viewSizeY - 1; j++)
                    if (View.Grid[i,j].CellReference != null)
                        newList.Add(View.Grid[i, j].CellReference);

            return newList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Int16 GetWidth()
        {
            return this.View.GetMapWidth();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Int16 GetHeight()
        {
            return this.View.GetMapHeight();
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
        internal IOffsetVector GetClosestRessourcePool(ICoordinates coordinatesFrom, ICell cell)
        {
            ICoordinates coordinatesTo = null;
            Int16? minDistanceSofar = (Int16)this.View.Grid.GetLength(0);

            for (int i = 0; i < this.View.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < this.View.Grid.GetLength(1); j++)
                {
                    if (this.View.Grid[i, j].RessourceLevel > 0)
                    {
                        var coord = new Coordinates();
                        coord.SetCoordinates((Int16)i, (Int16)j);
                        Int16? distTo = this.CellPositionInView.DistanceTo(coord);
                        if (distTo < minDistanceSofar)
                        {
                            minDistanceSofar = distTo;
                            coordinatesTo = coord;
                        }
                    }
                }
            }

            return coordinatesTo == null ? null : new OffsetVector(coordinatesFrom, coordinatesTo);
        }
    }
}
