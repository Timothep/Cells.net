using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;
using Cells.GameCore.Mapping.Tiles;
using System.Drawing;
using Cells.Model;

namespace Cells.Interfaces
{
    public interface ICell
    {
        CellAction Think();

        void Do(CellAction action);

        SurroundingView Sense();

        CellAction GetPreviousAction();

        AvailableActions GetRelativeMovment(ICoordinates coordinates);

        void DecreaseLife(Int16 malus = 1);

        Int16 GetLife();

        void Die();

        ICoordinates Position { get; set; }

        void SetLife(Int16 life);

        void SetTeam(Color teamColor);

        void SetBrain(IBrain brain);

        bool CanDivide();
    }
}
