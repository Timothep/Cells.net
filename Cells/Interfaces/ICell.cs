using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;
using Cells.GameCore.Mapping.Tiles;

namespace Cells.Interfaces
{
    public interface ICell
    {
        CellAction Think();

        void Do(CellAction action);

        SurroundingView Sense();

        CellAction GetPreviousAction();

        CellAction GetRelativeMovment(Utils.Coordinates coordinates);

        void DecreaseLife(Int16 malus = 1);

        Int16 GetLife();

        void Die();
    }
}
