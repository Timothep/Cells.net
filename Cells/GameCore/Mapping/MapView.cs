using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.GameCore;
using Cells.GameCore.Mapping.Tiles;

namespace Cells.GameCore.Mapping
{
    public class MapView: Map
    {
        public MapView(List<List<MapTile>> grid, short width = DefaultMapViewSquare, short height = DefaultMapViewSquare)
            : base (width, height)
        {
            this._grid = grid;
        }
    }
}
