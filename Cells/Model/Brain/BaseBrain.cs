using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cells.Interfaces;
using Cells.GameCore.Cells;

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
        protected ICell _cell { get; set; }

        /// <summary>
        /// Attach the cell to the brain
        /// </summary>
        /// <param name="cell">The cell to attach</param>
        public void SetCell(ICell cell)
        {
            this._cell = cell;
        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns>Returns one Action</returns>
        public virtual CellAction ChooseNextAction()
        {
            return CellAction.NONE;
        }
    }
}
