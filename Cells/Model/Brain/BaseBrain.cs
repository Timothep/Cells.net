using Cells.Interfaces;

namespace Cells.Model.Brain
{
    /// <summary>
    /// Base class containing the basic functionalities common to every brain
    /// </summary>
    public class BaseBrain
    {
        /// <summary>
        /// A reference to the cell the brain belongs to
        /// </summary>
        protected ICell Cell { get; set; }

        /// <summary>
        /// Attach the cell to the brain
        /// </summary>
        /// <param name="cell">The cell to attach</param>
        public void SetCell(ICell cell)
        {
            this.Cell = cell;
        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns>Returns one Action</returns>
        public virtual CellAction ChooseNextAction()
        {
            return new CellAction(AvailableActions.NONE);
        }
    }
}
