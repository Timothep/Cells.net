using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;
using Cells.Interfaces;
using Cells.Model.Mapping;

namespace Cells.GameCore.Mapping
{
    /// <summary>
    /// Class representing what the cell can "see"
    /// </summary>
    public class SurroundingView : ISurroundingView
    {
        private ICoordinates _centerOfView;
        
        // The view is a square centered on the cell
        private const short ViewSize = 3;

        private Map _view = new Map(ViewSize, ViewSize);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="coordinates"></param>
        public SurroundingView(ICoordinates coordinates, MapTile[,] view)
        {
            // Set the center coordinate
            _centerOfView = coordinates;
            _view = new Map(Convert.ToInt16(view.GetUpperBound(0)), Convert.ToInt16(view.GetUpperBound(1)));
            _view.InitializeGrid(view);
        }

        /// <summary>
        /// Function returning a list of all the cells present on the view
        /// </summary>
        /// <returns>An IList of ICell</returns>
        public IList<ICell> GetAllCells()
        {
            IList<ICell> newList = new List<ICell>();

            for (int i = 0; i < ViewSize - 1 ; i++)
                for (int j = 0; j < ViewSize - 1 ; j++)
                    if (_view.Grid[i,j].CellReference != null)
                        newList.Add(_view.Grid[i, j].CellReference);

            return newList;
        }
    }
}
