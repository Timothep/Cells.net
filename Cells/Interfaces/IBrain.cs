using Cells.GameCore.Cells;

namespace Cells.Interfaces
{
    interface IBrain
    {
        CellAction ChooseNextAction();
    }
}
