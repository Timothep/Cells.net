using System;
using System.Collections.Generic;
using Cells.Model.Mapping;

namespace Cells.Interfaces
{
    public interface IMap
    {
        Int16 GetMapHeight();
        
        Int16 GetMapWidth();
        
        MapTile[,] GetSubset(ICoordinates centerPoint, Int16 subWidth, Int16 subHeight);
        
        void InitializeGrid();
        
        void InitializeGrid(MapTile[,] view);
        
        IInternalCell GetCellAt(Int16 x, Int16 y);
        
        IList<ICoordinates> GetRessourcesList();
        
        MapTile GetTileAt(Int16 x, Int16 y);

        IInternalCell GetCellAt(ICoordinates targetCoordinates);
    }
}
