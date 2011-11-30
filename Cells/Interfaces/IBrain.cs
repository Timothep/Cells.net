using Cells.Model;

namespace Cells.Interfaces
{
    public interface IBrain
    {
        CellAction ChooseNextAction();
    }
}
