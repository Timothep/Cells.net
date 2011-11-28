using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace Cells.Interfaces
{
    public interface IWorld
    {
        SurroundingView GetSurroundingsView(ICell cell);

        void DropRessources(ICoordinates Position, short _life);

        void UnregisterCell(ICell cell);

        void LowerLandscape(ICoordinates Position);

        void RegisterCellMovement(ICoordinates oldCoordinates, ICoordinates newCoordinates, Color team);

        void RaiseLandscape(ICoordinates Position);

        void RemoveDeadCells();

        System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<ICoordinates, Color>> GetUpdatedElements();

        System.Collections.Generic.IEnumerable<ICell> GetCells();

        void Initialize(IList<String> availableBrains);

        void ResetMovementsList();
    }
}