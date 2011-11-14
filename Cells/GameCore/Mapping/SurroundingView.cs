using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;

namespace Cells.GameCore.Mapping
{
    /// <summary>
    /// Class representing what the cell can "see"
    /// </summary>
    public class SurroundingView
    {
        private Coordinates _centerOfView;
        
        // The view is a square centered on the cell
        private const short ViewSize = 3;

        private Map _cellsView = new Map(ViewSize, ViewSize);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="coordinates"></param>
        public SurroundingView(Coordinates coordinates)
        {
            // Set the center coordinate
            this.SetCenterCoordinates(coordinates);
        }

        // The various map extracts
        //CellsMapView
        //RessourcesMapView
        //PlantsMapView

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        private void SetCenterCoordinates(Coordinates coordinates)
        {
            _centerOfView = coordinates;
        }

        //internal void SetCellView(Map surroundingCellMap)
        //{
        //    throw new NotImplementedException();
        //}

        //internal void SetPlantView(Map surroundingPlantMap)
        //{
        //    throw new NotImplementedException();
        //}

        //internal void SetRessourceView(Map surroundingRessourceMap)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
