using System;
using Cells.GameCore.Mapping.Tiles;

namespace Cells.Interfaces
{
    public interface IMap
    {
        short GetMapHeight();
        int GetMapWidth();
        MapTile[,] GetSubset(ICoordinates centerPoint, short subWidth, short subHeight);
        void InitializeGrid();
        void InitializeGrid(MapTile[,] view);
    }
}
