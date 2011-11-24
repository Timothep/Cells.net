using Cells.GameCore.Cells;
using Cells.GameCore.Mapping;

namespace Cells.Interfaces
{
    public interface IWorld
    {

        SurroundingView GetSurroundingsView(Cell cell);

        void DropRessources(Utils.Coordinates Position, short _life);

        void UnregisterCell(Cell cell);

        void LowerLandscape(Utils.Coordinates Position);

        void RegisterCellMovement(Utils.Coordinates oldCoordinates, Utils.Coordinates newCoordinates, System.Drawing.Color team);

        void RaiseLandscape(Utils.Coordinates Position);
    }
}