using System;
using System.Collections.Generic;
using Cells.GameCore.Mapping;
using Cells.GameCore.Cells;
using Cells.Utils;
using Cells.Interfaces;
using System.ComponentModel.Composition;

namespace Cells.Brain
{
    /// <summary>
    /// CopyOfSwarmBrain class exporting itself as an IBrain
    /// </summary>
    [Export(typeof(IBrain))]
    public class CopyOfSwarmBrain: IBrain
    {
        private ICell _cell;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theCell">The cell that the brain should control</param>
        public CopyOfSwarmBrain()
        { 

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        public void SetCell(ICell cell)
        {
            this._cell = cell;
        }

        /// <summary>
        /// Function chosing the next action to be performed
        /// </summary>
        /// <returns></returns>
        public CellAction ChooseNextAction()
        {
            CellAction action;

            SurroundingView surroundings = _cell.Sense();
            IList<ICell> neighbors = surroundings.GetAllCells();

            // If the cell has no neighbours or in 1% of the cases => it goes random
            if (neighbors.Count == 0 || RandomGenerator.GetRandomInteger(100) == 0)
                action = GetRandomAction();
            else
            {
                // Else the cell goes toward a neighbour
                action = this._cell.GetRelativeMovment((neighbors[RandomGenerator.GetRandomInteger(neighbors.Count)] as Cell).Position);
            }

            return action;
        }

        /// <summary>
        /// Function randomly choosing among all the possible actions
        /// </summary>
        /// <returns>One of the possible action</returns>
        private CellAction GetRandomAction()
        {
            var randomNumber = (Int16)RandomGenerator.GetRandomInteger(5);

            switch (randomNumber)
            {
                case 0:
                    return CellAction.MOVERIGHT;
                case 1:
                    return CellAction.MOVELEFT;
                case 2:
                    return CellAction.MOVEDOWN;
                case 3:
                    return CellAction.MOVEUP;
                case 4:
                    return CellAction.NONE;
                default:
                    throw new Exception("Something went wrong with the random numbers");
            }
        }
    }
}
