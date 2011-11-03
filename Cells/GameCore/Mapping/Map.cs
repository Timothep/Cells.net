using System;
using System.Collections.Generic;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;

namespace Cells.GameCore.Mapping
{
    public class Map : List<List<MapTile>>
    {
        protected const short DefaultViewSize = 3;
        protected const short MinimumViewSize = 3;

        protected List<List<MapTile>> Grid = new List<List<MapTile>>();
        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// Creates an empty map structure
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Map(short width, short height)
        {
            _height = height;
            _width = width;
            CreateEmptyGrid();
        }

        private void CreateEmptyGrid()
        {
            // Create a square map of MapWidth / MapHeight size
            for (short col = 0; col < _width; col++)
            {
                var newColumn = new List<MapTile>();
                for (short row = 0; row < _height; row++)
                {
                    newColumn.Add(new MapTile(col, row));
                }
                Grid.Add(newColumn);
            }
        }

        //public Map GetSubView(Coordinates cellCoordinates)
        //{
        //    List<List<MapTile>> extract = GetMapExtract(cellCoordinates);
        //    return new MapView(extract);
        //}

        /// <summary>
        /// Function extracting a rectangular section of the map
        /// </summary>
        /// <param name="centerPoint">The point on which the section shall be centered</param>
        /// <param name="width">The odd numbered width of the section</param>
        /// <param name="height">The odd numbered height of the section</param>
        /// <returns>A 2D list MapTiles</returns>
        public Map GetMapExtract(Coordinates centerPoint, short width = DefaultViewSize, short height = DefaultViewSize)
        {
            // Check input parameters
            if (null == centerPoint
                || 0 == width % 2 || 0 == height % 2
                || width < MinimumViewSize || height < MinimumViewSize
                || height > _height || width > _width)
                return null;

            // Get the number of rows / columns that there are on the side of the cell on the extract
            short numberOfSideColumns = (short)Math.Truncate((float)(width/2));
            short numberOfSideRows = (short)Math.Truncate((float)(height/2));

            // Get the top left coordinates of the extract
            short smallGridMinX = centerPoint.X - numberOfSideColumns > 0 ? Convert.ToInt16(centerPoint.X - numberOfSideColumns) : (Int16)0;
            short smallGridMinY = centerPoint.Y - numberOfSideRows > 0 ? Convert.ToInt16(centerPoint.Y - numberOfSideRows) : (Int16)0;

            // Get the bottom right coordinates of the extract
            short smallGridMaxX = smallGridMinX + width > Grid.Count ? Convert.ToInt16(Grid.Count): Convert.ToInt16(smallGridMinX + width);
            short smallGridMaxY = smallGridMinY + height > Grid[0].Count ? Convert.ToInt16(Grid[0].Count) : Convert.ToInt16(smallGridMinY + height);

            return GetSubArray(Grid, smallGridMinX, smallGridMinY, smallGridMaxX, smallGridMaxY);
        }

        /// <summary>
        /// Extracts a sub array from the array passed as a parameter
        /// </summary>
        /// <param name="grid">The 2D source array</param>
        /// <param name="xMin">The "top left" x bound of the array to extract</param>
        /// <param name="xMax">The "top left" y bound of the array to extract</param>
        /// <param name="yMin">The "bottom right" x bound of the array to extract</param>
        /// <param name="yMax">The "bottom right" y bound of the array to extract</param>
        /// <returns>The sub 2D array</returns>
        private Map GetSubArray(List<List<MapTile>> grid, Int16 xMin, Int16 xMax, Int16 yMin, Int16 yMax)
        {
            var newMap = new Map((Int16)(xMax-xMin),(Int16)(yMax-yMin));

            for (int i = xMin; i < xMax; i++)
            {
                newMap.Grid.Add(new List<MapTile>());
                for (int j = yMin; j < yMax; j++)
                {
                    newMap.Grid[i - xMin].Add(grid[i][j]);
                }
            }

            return newMap;
        }
    }
}
