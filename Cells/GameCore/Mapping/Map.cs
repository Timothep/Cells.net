using System;
using System.Collections.Generic;
using Cells.GameCore.Mapping.Tiles;
using Cells.Utils;

namespace Cells.GameCore.Mapping
{
    public class Map
    {
        protected const short DefaultMapViewSquare = 3;

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
            for (int col = 0; col < _width; col++)
            {
                List<MapTile> newColumn = new List<MapTile>();
                for (int row = 0; row < _height; row++)
                {
                    newColumn.Add(new MapTile(col, row));
                }
                Grid.Add(newColumn);
            }
        }

        public MapView CreateMapView(Coordinates cellCoordinates)
        {
            List<List<MapTile>> extract = GetMapExtract(cellCoordinates);
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

            short smallGridMaxX = smallGridMinX + width > Grid.Count ? Convert.ToInt16(Grid.Count): Convert.ToInt16(smallGridMinX + width);
            short smallGridMaxY = smallGridMinY + height > Grid[0].Count ? Convert.ToInt16(Grid[0].Count) : Convert.ToInt16(smallGridMinY + height);

            // Extract the elements from the original grid
            var extract = new List<List<MapTile>>();
            for (int i = smallGridMinX; i < smallGridMaxX ; i++)
            {
                extract.Add(new List<MapTile>());
                for (int j = smallGridMinY; j < smallGridMaxY ; j++)
                {
                    extract[i - smallGridMinX].Add(Grid[i][j]);
                }
            }

            return extract;
        }
    }
}
