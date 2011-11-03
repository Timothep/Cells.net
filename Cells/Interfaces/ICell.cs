using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;
using Cells.GameCore.Mapping.Tiles;

namespace Cells.Interfaces
{
    interface ICell
    {
        CellAction Think();

        void Do(CellAction action);

        IDictionary<String, MapView> Sense();

        CellAction GetPreviousAction();
    }
}
