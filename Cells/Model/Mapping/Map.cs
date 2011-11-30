using System;
using System.Collections.Generic;
using Cells.Interfaces;
using Cells.Utils;

namespace Cells.Model.Mapping
{
    public class Map : IMap
    {
        private const Int16 MinimumMapSize = 3;
        private readonly short Width;
        private readonly short Height;
        private IList<ICoordinates> ressourcesList = new List<ICoordinates>();

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
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public MapTile GetTileAt(Int16 x, Int16 y)
        {
            return this.Grid[x, y];
        }

        /// <summary>
        /// Function extracting a rectangular section of the map
        /// </summary>
        /// <param name="centerPoint">The point on which the section shall be centered</param>
        /// <param name="subWidth">The odd numbered width of the section</param>
        /// <param name="subHeight">The odd numbered height of the section</param>
        /// <returns>A 2D list MapTiles</returns>
        public MapTile[,] GetSubset(ICoordinates centerPoint, short subWidth, short subHeight)
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
            // Get the bottom right coordinates of the extract (-1 since we are working with a 0 based array)
            var smallGridMaxX = smallGridMinX + subWidth >= Grid.GetLength(0) ? Convert.ToInt16(Grid.GetLength(0) - 1) : Convert.ToInt16(smallGridMinX + subWidth -1);
            var smallGridMaxY = smallGridMinY + subHeight >= Grid.GetLength(1) ? Convert.ToInt16(Grid.GetLength(1) - 1) : Convert.ToInt16(smallGridMinY + subHeight -1);
            
            MapTile[,] subArray = GetSubArray(smallGridMinX, smallGridMaxX, smallGridMinY, smallGridMaxY);

            if (subArray == null)
            {
            }

            return subArray;
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
            ICoordinates cMin = new Coordinates();
            cMin.SetCoordinates(xMin, yMin);
            ICoordinates cMax = new Coordinates();
            cMax.SetCoordinates(xMax, yMax);

            if (CoordinatesAreValid(cMin) && CoordinatesAreValid(cMax))
            {
                // Create a new map (add +1 to the dimention since it is a 0 based array)
                var newMap = new MapTile[(Int16)(xMax - xMin +1), (Int16)(yMax - yMin + 1)];
                for (int i = xMin; i <= xMax; i++)
                    for (int j = yMin; j <= yMax; j++)
                        newMap[i - xMin, j - yMin] = Grid[i, j];
                return newMap;
            }
            else
                throw new Exception();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public IInternalCell GetCellAt(Int16 x, Int16 y)
        {
            return this.Grid[x, y].CellReference;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetCoordinates"></param>
        /// <returns></returns>
        public IInternalCell GetCellAt(ICoordinates targetCoordinates)
        {
            return this.Grid[targetCoordinates.X, targetCoordinates.Y].CellReference;
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
        public Int16 GetMapWidth()
        {
            return Grid != null ? Convert.ToInt16(Grid.GetUpperBound(0)) : (Int16)0;
        }

        /// <summary>
        /// Function registering a cell on the map
        /// </summary>
        /// <param name="newCell">The cell</param>
        internal void ImplantCell(IInternalCell newCell)
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
        internal void MoveCell(ICoordinates oldCoordinates, ICoordinates newCoordinates)
        {
            if (CoordinatesAreValid(oldCoordinates) && CoordinatesAreValid(newCoordinates))
            {
                Grid[newCoordinates.X, newCoordinates.Y].CellReference = Grid[oldCoordinates.X, oldCoordinates.Y].CellReference;
                Grid[oldCoordinates.X, oldCoordinates.Y].CellReference = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newCoordinates"></param>
        /// <returns></returns>
        private bool CoordinatesAreValid(ICoordinates newCoordinates)
        {
            if (newCoordinates.X < 0
                || newCoordinates.Y < 0
                || newCoordinates.X >= this.Grid.GetLength(0)
                || newCoordinates.Y >= this.Grid.GetLength(1))
                return false;

            return true;
        }

        /// <summary>
        /// Function implanting ressources on the map
        /// </summary>
        /// <param name="coordinates">The coordinates where to implant the ressources</param>
        /// <param name="ressourceLevel">The amount of ressources to implant</param>
        /// <param name="growthRate">The growth rate of the ressources (per tick, 0 per default)</param>
        internal void ImplantRessources(ICoordinates coordinates, Int16 ressourceLevel, Int16 growthRate = 0)
        {
            this.Grid[coordinates.X, coordinates.Y].GrowthRate = growthRate;
            this.Grid[coordinates.X, coordinates.Y].RessourceLevel = ressourceLevel;
            this.ressourcesList.Add(coordinates);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        internal void RaiseLandscape(ICoordinates position)
        {
            Grid[position.X, position.Y].Altitude++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        internal void LowerLandscape(ICoordinates position)
        {
            Grid[position.X, position.Y].Altitude--;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        internal Int16 GetLandscapeHeight(ICoordinates position)
        {
            return Grid[position.X, position.Y].Altitude;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="ressources"></param>
        internal void IncreaseRessources(ICoordinates position, short ressources)
        {
            Grid[position.X, position.Y].RessourceLevel += ressources;
            
            if (!this.ressourcesList.Contains(position))
                this.ressourcesList.Add(position);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="ressources"></param>
        internal void DecreaseRessources(ICoordinates position, Int16 ressources)
        {
            Grid[position.X, position.Y].RessourceLevel -= ressources;

            if (Grid[position.X, position.Y].RessourceLevel < 0)
            {
                Grid[position.X, position.Y].RessourceLevel = 0;
                this.ressourcesList.Remove(position);
            }
        }

        /// <summary>
        /// Removes a cell from the grid
        /// </summary>
        /// <param name="cellToRemove"></param>
        internal void RemoveCell(IInternalCell cellToRemove)
        {
            if (cellToRemove != null)
                Grid[cellToRemove.Position.X, cellToRemove.Position.Y].CellReference = null;
            else
                throw new Exception("Cannot remove a non existing cell");
        }

        public IList<ICoordinates> GetRessourcesList()
        {
            return this.ressourcesList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        internal Int16 GetAmountOfRessourcesLeft(ICoordinates coordinates)
        {
            return this.Grid[coordinates.X, coordinates.Y].RessourceLevel;
        }
    }
}
