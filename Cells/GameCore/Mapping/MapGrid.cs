using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.GameCore.Mapping.Tiles;

namespace Cells.GameCore.Mapping
{
    public class MapGrid
    {
        protected MapTile[,] Grid;

        public MapGrid(short width, short height)
        {
            this.Grid = new MapTile[width, height];
        }

    }
}
