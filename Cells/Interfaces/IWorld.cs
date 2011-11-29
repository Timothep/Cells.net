using System.Drawing;
using System;
using System.Collections.Generic;
using Cells.Model.Cells;
using Cells.Model.Mapping;

namespace Cells.Interfaces
{
    public interface IWorld
    {
        SurroundingView GetSurroundingsView(ICell cell);

        void DropRessources(ICoordinates Position, short _life);

        void UnregisterCell(ICell cell);

        void LowerLandscape(ICoordinates Position);

        void RegisterCellMovement(ICoordinates oldCoordinates, ICoordinates newCoordinates, DisplayQualifier team);

        void RaiseLandscape(ICoordinates Position);

        void RemoveDeadCells();

        IEnumerable<ICell> GetCells();

        void Initialize(IList<String> availableBrains);

        void ResetMovementsList();

        void CreateSpawns(short spawnLife, Cell cell);

        void AddNewlyCreatedCellsToTheGame();

        void ReduceRessources(ICoordinates coordinates, short lifeBonus);

        Int16 GetAmountOfRessourcesLeft(ICoordinates coordinates);

        IMap GetMap();
    }
}