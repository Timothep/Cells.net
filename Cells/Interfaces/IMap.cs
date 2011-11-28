using System;
using Cells.GameCore.Mapping.Tiles;

namespace Cells.Interfaces
{
    public interface IMap
    {
        Int16 GetMapHeight();
        Int16 GetMapWidth();
        MapTile[,] GetSubset(ICoordinates centerPoint, Int16 subWidth, Int16 subHeight);
        void InitializeGrid();
        void InitializeGrid(MapTile[,] view);
    }
}
