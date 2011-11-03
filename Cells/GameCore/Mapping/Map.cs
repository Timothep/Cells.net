using System;
using System.Collections.Generic;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;

namespace Cells.GameCore.Mapping
{
    public class Map
    {
        protected const short DefaultMapViewSquare = 3;

        protected List<List<MapTile>> _grid = new List<List<MapTile>>();
        private int _width, _height;

        /// <summary>
        /// Creates an empty map structure
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Map(short width, short height)
        {
            this._height = height;
            this._width = width;

            this.CreateEmptyGrid();
        }

        private void CreateEmptyGrid()
        {
            // Create a square map of MapWidth / MapHeight size
            for (int col = 0; col < this._width; col++)
            {
                List<MapTile> newColumn = new List<MapTile>();
                for (int row = 0; row < this._height; row++)
                {
                    newColumn.Add(new MapTile((int)col, (int)row));
                }
                this._grid.Add(newColumn);
            }
        }

        public MapView CreateMapView(Coordinates cellCoordinates)
        {
            List<List<MapTile>> extract = this.GetMapExtract(cellCoordinates);
            return new MapView(extract);
        }

        /// <summary>
        /// Function extracting a rectangular section of the map
        /// </summary>
        /// <param name="centerPoint">The point on which the section shall be centered</param>
        /// <param name="width">The odd numbered width of the section</param>
        /// <param name="height">The odd numbered height of the section</param>
        /// <returns>A 2D list MapTiles</returns>
        public List<List<MapTile>> GetMapExtract(Coordinates centerPoint, short width = DefaultMapViewSquare, short height = DefaultMapViewSquare)
        {
            // Check input parameters
            if (null == centerPoint
                || 0 == width % 2 || 0 == height % 2 
                || width < 3 || height < 3
                || height > _height || width > _width)
                return null;

            // Get the start positions
            short numberOfSideColumns = (short)Math.Truncate((float)(width/2));
            short numberOfSideRows = (short)Math.Truncate((float)(height/2));
            
            short smallGridMinX = centerPoint.X - numberOfSideColumns > 0 ? Convert.ToInt16(centerPoint.X - numberOfSideColumns) : (Int16)0;
            short smallGridMinY = centerPoint.Y - numberOfSideRows > 0 ? Convert.ToInt16(centerPoint.Y - numberOfSideRows) : (Int16)0;

            short smallGridMaxX = smallGridMinX + width > _grid.Count ? Convert.ToInt16(_grid.Count): Convert.ToInt16(smallGridMinX + width);
            short smallGridMaxY = smallGridMinY + height > _grid[0].Count ? Convert.ToInt16(_grid[0].Count) : Convert.ToInt16(smallGridMinY + height);

            // Extract the elements from the original grid
            List<List<MapTile>> extract = new List<List<MapTile>>();
            for (int i = smallGridMinX; i < smallGridMaxX ; i++)
            {
                extract.Add(new List<MapTile>());
                for (int j = smallGridMinY; j < smallGridMaxY ; j++)
                {
                    extract[i - smallGridMinX].Add(this._grid[i][j]);
                }
            }

            return extract;
        }

        ///// <summary>
        ///// Get the width of the created map
        ///// </summary>
        //public int GetGridWidth()
        //{
        //    if (this._grid == null)
        //        throw new NullReferenceException();
        //    return this._grid.Count;
        //}

        ///// <summary>
        ///// Get the height of the created map
        ///// </summary>
        //public int GetGridHeight()
        //{
        //    if (this._grid == null)
        //        throw new NullReferenceException();
        //    return this._grid[0].Count;
        //}

        ///// <summary>
        ///// Retrieves one element of the Map
        ///// </summary>
        ///// <param name="x">x coordinate of the element to retrieve</param>
        ///// <param name="y">y coordinate of the element to retrieve</param>
        ///// <returns>The MapCell element</returns>
        //public MapTile GetElement(int x, int y)
        //{
        //    if (this._grid == null || x >= this._grid.Count)
        //        return null;
        //    else
        //    {
        //        List<MapTile> column = this._grid.ElementAt(x);

        //        if (column == null || y >= column.Count)
        //            return null;

        //        return column.ElementAt(y);
        //    }
        //}

        ///// <summary>
        ///// Function finding a random free spot on the map
        ///// </summary>
        ///// <returns>The coordinates of the spot</returns>
        //public Coordinates GetFreeSpot()
        //{
        //    int x, y = 0;
        //    MapTile spot = null;
        //    List<MapTile> possibilities = GetAllMapTiles();

        //    do
        //    {
        //        if (possibilities.Count == 0)
        //            return null;

        //        spot = possibilities[GetRandomInt(possibilities.Count)];

        //        if (!this.GetElement(spot.GetPosition().X, spot.GetPosition().Y).IsOccupied())
        //            break;
        //        else
        //        {
        //            possibilities.Remove(spot);
        //            spot = null;
        //        }
        //    } while (possibilities.Count > 0);
            
        //    return spot == null ? null : spot.GetPosition();
        //}

        ///// <summary>
        ///// Returns a list of all the coordinates of the map concatenated like "x y"
        ///// </summary>
        //public List<MapTile> GetAllMapTiles()
        //{
        //    List<MapTile> allCells = new List<MapTile>();

        //    for (int col = 0; col < this._width; col++)
        //    {
        //        for (int row = 0; row < this._height; row++)
        //        {
        //            allCells.Add(new MapTile(col,row));
        //        }
        //    }
        //    return allCells;
        //}

        ///// <summary>
        ///// Function generating a new random number (integer)
        ///// </summary>
        ///// <param name="maxValue">(Optional) Maximum value of the integer (default MaxInt)</param>
        ///// <returns>A random integer</returns>
        //private int GetRandomInt(int maxValue = Int32.MaxValue)
        //{
        //    return _randomGen.Next(maxValue);
        //}
    }
}
