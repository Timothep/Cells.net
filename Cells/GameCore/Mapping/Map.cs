using System;
using System.Collections.Generic;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping.Tiles;
using Cells.Interfaces;
using Cells.Utils;

namespace Cells.GameCore.Mapping
{
    public class Map
    {
        private const Int16 MinimumMapSize = 3;

        private readonly short Width;
        private readonly short Height;

        public MapTile[,] Grid;
        
        /// <summary>
        /// Creates an empty map structure
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Map(short width, short height)
        {
            this.Height = height;
            this.Width = width;
            this.Grid = new MapTile[Width, Height];
            InitializeGrid();
        }

        /// <summary>
        /// Populates the grid with MapTiles
        /// </summary>
        public void InitializeGrid()
        {
            // Create a square map of MapWidth / MapHeight size
            for (short col = 0; col < Width; col++)
            {
                for (short row = 0; row < Height; row++)
                {
                    var tile = new MapTile();
                    tile.SetPositions(col, row);
                    Grid[col, row] = tile;
                }
            }
        }

        /// <summary>
        /// Initializes the grid of the map with the grid passed as a reference
        /// </summary>
        /// <param name="view">The grid that is to become this map's grid</param>
        public void InitializeGrid(MapTile[,] view)
        {
            Grid = view;
        }

        /// <summary>
        /// Function extracting a rectangular section of the map
        /// </summary>
        /// <param name="centerPoint">The point on which the section shall be centered</param>
        /// <param name="subWidth">The odd numbered width of the section</param>
        /// <param name="subHeight">The odd numbered height of the section</param>
        /// <returns>A 2D list MapTiles</returns>
        public MapTile[,] GetSubset(Coordinates centerPoint, short subWidth, short subHeight)
        {
            // Check input parameters
            if (null == centerPoint
                || 0 == subWidth % 2 || 0 == subHeight % 2
                || subWidth < MinimumMapSize || subHeight < MinimumMapSize
                || subHeight > Height || subWidth > Width)
                return null;
            
            // Get the number of rows / columns that there are on the side of the cell on the extract
            var numberOfSideColumns = (short)Math.Truncate((float)(subWidth / 2));
            var numberOfSideRows = (short) Math.Truncate((float) (subHeight/2));
            // Get the top left coordinates of the extract
            var smallGridMinX = centerPoint.X - numberOfSideColumns > 0 ? Convert.ToInt16(centerPoint.X - numberOfSideColumns) : (Int16)0;
            var smallGridMinY = centerPoint.Y - numberOfSideRows > 0 ? Convert.ToInt16(centerPoint.Y - numberOfSideRows) : (Int16)0;
            // Get the bottom right coordinates of the extract
            var smallGridMaxX = smallGridMinX + subWidth > Grid.Length ? Convert.ToInt16(Grid.GetUpperBound(0)) : Convert.ToInt16(smallGridMinX + subWidth);
            var smallGridMaxY = smallGridMinY + subHeight > Grid.LongLength ? Convert.ToInt16(Grid.GetUpperBound(1)) : Convert.ToInt16(smallGridMinY + subHeight);
            
            return GetSubArray(smallGridMinX, smallGridMaxX, smallGridMinY, smallGridMaxY);
        }

        /// <summary>
        /// Extracts a sub array from the array passed as a parameter
        /// </summary>
        /// <param name="xMin">The "top left" x bound of the array to extract</param>
        /// <param name="xMax">The "top left" y bound of the array to extract</param>
        /// <param name="yMin">The "bottom right" x bound of the array to extract</param>
        /// <param name="yMax">The "bottom right" y bound of the array to extract</param>
        /// <returns>The sub 2D array</returns>
        private MapTile[,] GetSubArray(Int16 xMin, Int16 xMax, Int16 yMin, Int16 yMax)
        {
            var newMap = new MapTile[(Int16)(xMax - xMin), (Int16)(yMax - yMin)];
            for (int i = xMin; i < xMax; i++)
                for (int j = yMin; j < yMax; j++)
                    newMap[i - xMin ,j - yMin] = Grid[i,j];

            return newMap;
        }

        /// <summary>
        /// Returns the height of the map
        /// </summary>
        /// <returns></returns>
        public Int16 GetMapHeight()
        {
            return Grid != null ? Convert.ToInt16(Grid.GetUpperBound(1)) : (Int16)0;
        }

        /// <summary>
        /// Returns the width of the map
        /// </summary>
        /// <returns></returns>
        public int GetMapWidth()
        {
            return Grid != null ? Convert.ToInt16(Grid.GetUpperBound(0)) : 0;
        }

        /// <summary>
        /// Function registering a cell on the map
        /// </summary>
        /// <param name="newCell">The cell</param>
        internal void ImplantCell(Cells.Cell newCell)
        {
            if (newCell != null)
                Grid[newCell.Position.X, newCell.Position.Y].CellReference = newCell;
            else
                throw new Exception("Cannot implant a non existing cell");
        }

        /// <summary>
        /// Function moving a cell on the map
        /// </summary>
        /// <param name="oldCoordinates">The coordinates where the cell is moving from</param>
        /// <param name="newCoordinates">The coordinates where the cell is moving to</param>
        internal void MoveCell(Coordinates oldCoordinates, Coordinates newCoordinates)
        {
            Grid[newCoordinates.X, newCoordinates.Y].CellReference = Grid[oldCoordinates.X, oldCoordinates.Y].CellReference;
            Grid[oldCoordinates.X, oldCoordinates.Y].CellReference = null;
        }

        /// <summary>
        /// Function implanting ressources on the map
        /// </summary>
        /// <param name="coordinates">The coordinates where to implant the ressources</param>
        /// <param name="ressourceLevel">The amount of ressources to implant</param>
        /// <param name="growthRate">The growth rate of the ressources (per tick, 0 per default)</param>
        internal void ImplantRessources(Coordinates coordinates, Int16 ressourceLevel, Int16 growthRate = 0)
        {
            Grid[coordinates.X, coordinates.Y].GrowthRate = growthRate;
            Grid[coordinates.X, coordinates.Y].RessourceLevel = ressourceLevel;
        }

        internal void RaiseLandscape(Coordinates position)
        {
            Grid[position.X, position.Y].Height++;
        }

        internal void LowerLandscape(Coordinates position)
        {
            Grid[position.X, position.Y].Height--;
        }

        internal Int16 GetLandscapeHeight(Coordinates position)
        {
            return Grid[position.X, position.Y].Height;
        }

        internal void IncreaseRessources(Coordinates position, short ressources)
        {
            Grid[position.X, position.Y].RessourceLevel += ressources;
        }

        /// <summary>
        /// Removes a cell from the grid
        /// </summary>
        /// <param name="cellToRemove"></param>
        internal void RemoveCell(Cell cellToRemove)
        {
            if (cellToRemove != null)
                Grid[cellToRemove.Position.X, cellToRemove.Position.Y].CellReference = null;
            else
                throw new Exception("Cannot remove a non existing cell");
        }
    }
}
