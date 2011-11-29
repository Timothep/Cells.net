using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Cells.Model;
using Cells.Model.Mapping;

namespace Cells.Interfaces
{
    public interface ICell
    {
        CellAction Think();

        void Do(CellAction action);

        SurroundingView Sense();

        CellAction GetPreviousAction();

        AvailableActions GetRelativeMovment(ICoordinates coordinates);

        AvailableActions GetRelativeMovment(IOffsetVector coordinates);

        void DecreaseLife(Int16 malus = 1);

        Int16 GetLife();

        void Die();

        ICoordinates Position { get; set; }

        void SetLife(Int16 life);

        void SetTeam(Color teamColor);

        void SetBrain(IBrain brain);

        bool CanDivide();

        //bool CanMoveRight();
        
        //bool CanMoveLeft();
        
        //bool CanMoveDown();
        
        //bool CanMoveUp();
    }
}
